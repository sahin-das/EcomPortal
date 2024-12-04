namespace EcomPortal.Models.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public required string BuyerName { get; set; }
        public required string ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public required decimal TotalBill { get; set; }
    }
}
