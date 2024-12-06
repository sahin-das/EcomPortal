using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcomPortal.Models.Entities
{
    public class Order
    {
        public Guid Id { get; init; }
        
        [Required, StringLength(100, ErrorMessage = "Buyer name must be between 1 and 100 characters.")]
        public required string BuyerName { get; set; }
        
        [Required, StringLength(100, ErrorMessage = "Item name must be between 1 and 100 characters.")]
        public required string ItemName { get; set; }
        
        [StringLength(500, ErrorMessage = "Item description can't exceed 500 characters.")]
        public string? ItemDescription { get; set; }
        
        [Required, Range(0.01, double.MaxValue, ErrorMessage = "Total bill must be greater than zero.")]
        
        public required Guid UserId { get; init; }

        [JsonIgnore]
        public User User { get; init; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total bill must be greater than zero.")]
        public decimal TotalBill { get; set; }

        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;

        public ICollection<OrderProduct> OrderProducts { get; init; } = [];
    }
}
