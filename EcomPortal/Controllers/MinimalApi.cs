using EcomPortal.Models.Dtos.Order;
using EcomPortal.Services;

namespace EcomPortal.Controllers
{
    public static class MinimalApi
    {
        public static void MapOrderEndpoints(this WebApplication app)
        {
            app.MapGet("/api/orders/", GetAllOrders).WithName("GetAllOrders");
            app.MapGet("/api/orders/{id:guid}", GetOrderById).WithName("GetOrderById");
            app.MapPost("/api/orders/", AddOrder).WithName("AddOrder");
            app.MapPut("/api/orders/{id:guid}", UpdateOrder).WithName("UpdateOrder");
            app.MapDelete("/api/orders/{id:guid}", DeleteOrder).WithName("DeleteOrder");

            static async Task<IResult> GetAllOrders(OrderService orderService)
            {
                var orders = await orderService.GetAllAsync();
                return Results.Ok(orders);
            }

            static async Task<IResult> GetOrderById(Guid id, OrderService orderService)
            {
                var order = await orderService.GetByIdAsync(id);
                return order != null ? Results.Ok(order) : Results.NotFound($"Order with ID {id} not found.");
            }

            static async Task<IResult> AddOrder(AddOrderDto request, OrderService orderService)
            {
                if (request == null)
                {
                    return Results.BadRequest("Request body is null.");
                }

                var order = await orderService.CreateAsync(request);
                return Results.Ok(order);
            }

            static async Task<IResult> UpdateOrder(Guid id, UpdateOrderDto request, OrderService orderService)
            {
                try
                {
                    var updatedOrder = await orderService.UpdateAsync(id, request);
                    return Results.Ok(updatedOrder);
                }
                catch (Exception ex)
                {
                    return Results.NotFound(ex.Message);
                }
            }

            static async Task<IResult> DeleteOrder(Guid id, OrderService orderService)
            {
                try
                {
                    await orderService.DeleteAsync(id);
                    return Results.Ok("Deleted Successfully.");
                }
                catch (Exception ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            }
        }
    }
}
