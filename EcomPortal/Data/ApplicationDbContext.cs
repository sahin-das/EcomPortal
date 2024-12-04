using EcomPortal.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcomPortal.Data;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Order> Orders { get; set; }
}
