using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeClub.Infrastructure;

public class CoffeeClubContext : DbContext
{
    public CoffeeClubContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<DrinkOrder> DrinkOrders { get; set; } = default!;

    public DbSet<Order> Orders { get; set; } = default!;

    public DbSet<CoffeeBean> CoffeeBeans { get; set; } = default!;

    // protected override void OnConfiguring(DbContextOptionsBuilder options)
    //     => options.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true");
}
