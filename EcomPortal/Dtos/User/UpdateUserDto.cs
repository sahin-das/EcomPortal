namespace EcomPortal.Models.Dtos.User
{
    public class UpdateUserDto
    {
        public required string Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
