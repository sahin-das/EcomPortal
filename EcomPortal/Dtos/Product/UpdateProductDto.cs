namespace EcomPortal.Models.Dtos.Product
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }
        public required decimal Price { get; set; }
    }
}
