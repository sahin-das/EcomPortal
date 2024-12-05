using EcomPortal.Models.Dtos.OrderProduct;

namespace EcomPortal.Models.Dtos.Order
{
    public class AddOrderDto
    {
        public string BuyerName { get; set; } = null!;
        public string ItemName { get; set; } = null!;
        public string? ItemDescription { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalBill { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; } = new List<OrderProductDto>();
    }
}
