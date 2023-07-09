using CoffeClub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeClub.Infrastructure;
public class CoffeeClubContextFactory : IDesignTimeDbContextFactory<CoffeeClubContext>
{
    public CoffeeClubContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoffeeClubContext>();
        optionsBuilder.UseSqlServer("Server=localhost;User Id=SA;Password=your_password1234;Database=CoffeeClub;TrustServerCertificate=true");

        return new CoffeeClubContext(optionsBuilder.Options);
    }
}
