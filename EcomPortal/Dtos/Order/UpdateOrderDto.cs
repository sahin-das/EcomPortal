using EcomPortal.Models.Dtos.OrderProduct;

namespace EcomPortal.Models.Dtos.Order

{
    public class UpdateOrderDto
    {
        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}

