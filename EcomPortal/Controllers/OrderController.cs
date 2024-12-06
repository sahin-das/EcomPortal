using EcomPortal.Models.Entities;
using EcomPortal.Models.Dtos.Order;
using EcomPortal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcomPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController(IGenericService<Order, AddOrderDto, UpdateOrderDto> OrderService, ILogger<OrderController> logger) : ControllerBase
    {
        private readonly IGenericService<Order, AddOrderDto, UpdateOrderDto> _OrderService = OrderService;
        private readonly ILogger<OrderController> _logger = logger;

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            _logger.LogInformation("Executing Get method");
            var Orders = await _OrderService.GetAllAsync();
            return Ok(Orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var Order = await _OrderService.GetByIdAsync(id);
            if (Order == null)
            {
                return NotFound($"Order with ID {id} not found.");
            }
            return Ok(Order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] AddOrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var Order = await _OrderService.CreateAsync(request);
            return Ok(Order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody] UpdateOrderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var Order = await _OrderService.UpdateAsync(id, request);
                return Ok(Order);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            try
            {
                await _OrderService.DeleteAsync(id);
                return Ok("Deleted Successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
