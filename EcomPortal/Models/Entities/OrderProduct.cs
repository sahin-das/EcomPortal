using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcomPortal.Models.Entities;

public class OrderProduct
{
    public Guid OrderId { get; set; }

    [JsonIgnore]
    public Order Order { get; set; } = null!;

    public Guid ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } = null!;

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
    public int Quantity { get; set; }
}