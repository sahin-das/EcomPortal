namespace EcomPortal.Models
{
    public class AddUserDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
