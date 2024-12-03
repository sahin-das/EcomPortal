using EcomPortal.Data;
using EcomPortal.Models;
using EcomPortal.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    public static class OrderController
    {
        public static void MapOrderEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapGet("/api/orders", GetAllOrders);
            endpoints.MapGet("/api/orders/{id}", GetOrderById);
            endpoints.MapPost("/api/orders", CreateOrder);
            endpoints.MapPut("/api/orders/{id}", UpdateOrder);
            endpoints.MapDelete("/api/orders/{id}", DeleteOrder);
        }

        private static IResult GetAllOrders(ApplicationDbContext dbContext)
        {
            var orders = dbContext.Orders.ToList();
            if (orders == null)
            {
                return Results.NotFound("No orders found");
            }
            return Results.Ok(orders);
        }
        
        private static async Task<IResult> GetOrderById(Guid id, ApplicationDbContext dbContext)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return Results.NotFound("No orders found");
            }
            return Results.Ok(order);
        }

        private static async Task<IResult> CreateOrder([FromBody] CreateOrderRequest request, ApplicationDbContext dbContext)
        {
            var order = new Order()
            {
                BuyerName = request.BuyerName,
                ItemName = request.ItemName,
                ItemDescription = request.ItemDescription,
                TotalBill = request.TotalBill
            };
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return Results.Ok(order);
        }

        private static async Task<IResult> UpdateOrder(Guid id, [FromBody] UpdateOrderRequest request, ApplicationDbContext dbContext)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return Results.NotFound("No orders found");
            }
            order.BuyerName = request.BuyerName;
            order.ItemName = request.ItemName;
            order.ItemDescription = request.ItemDescription;
            order.TotalBill = request.TotalBill;

            await dbContext.SaveChangesAsync();
            return Results.Ok(order);
        }

        private static async Task<IResult> DeleteOrder(Guid id, ApplicationDbContext dbContext)
        {
            var order = await dbContext.Orders.FindAsync(id);
            if (order == null)
            {
                return Results.NotFound("No orders found");
            }
            dbContext.Remove(order);
            await dbContext.SaveChangesAsync();
            return Results.Ok("Order deleted succesfully.");
        }
    }
}
