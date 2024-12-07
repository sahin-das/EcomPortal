using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcomPortal.Models.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        [Required, StringLength(100, ErrorMessage = "Name must be between 1 and 100 characters.")]
        public required string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address."), StringLength(100)]
        public string? Email { get; set; }
        [Phone(ErrorMessage = "Invalid phone number."), StringLength(13)]
        public string? Phone { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; } = true;
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}