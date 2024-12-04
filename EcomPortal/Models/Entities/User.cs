namespace EcomPortal.Models.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone {  get; set; }
    }
}
