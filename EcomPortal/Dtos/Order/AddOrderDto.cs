using EcomPortal.Models.Dtos.OrderProduct;

namespace EcomPortal.Models.Dtos.Order
{
    public class AddOrderDto
    {
        public Guid UserId { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}
