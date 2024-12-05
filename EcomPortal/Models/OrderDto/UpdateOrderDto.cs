namespace EcomPortal.Models.OrderRequest
{
    public class UpdateOrderDto
    {
        public required string BuyerName { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public required decimal TotalBill { get; set; }
    }
}
