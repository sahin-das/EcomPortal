using System.Text.Json.Serialization;

namespace EcomPortal.Models.Entities
{
    public class Order
    {
        public Guid Id { get; init; }
        [JsonIgnore]
        public Guid UserId { get; init; }
        public User User { get; init; } = null!;
        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public DateTime CreatedDate { get; init; } = DateTime.UtcNow;
    }
}