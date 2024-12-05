using EcomPortal.Data;
using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    // minimal to normal, implment logs, service and repo...
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

        private static async Task<IResult> CreateOrder([FromBody] AddOrderDto request, ApplicationDbContext dbContext)
        {
            var order = new Order()
            {
                UserId = request.UserId,
                BuyerName = request.BuyerName,
                ItemName = request.ItemName,
                ItemDescription = request.ItemDescription,
                TotalBill = request.TotalBill
            };
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
            return Results.Ok(order);
        }

        private static async Task<IResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDto request, ApplicationDbContext dbContext)
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
            return Results.Ok("Order deleted successfully.");
        }
    }
}
