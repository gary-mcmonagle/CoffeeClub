using CoffeClub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CoffeeClub.Infrastructure;
public class CoffeeClubContextFactory : IDesignTimeDbContextFactory<CoffeeClubContext>
{
    public CoffeeClubContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CoffeeClubContext>();
        optionsBuilder.UseSqlServer("Server=tcp:coffeeclubprod.database.windows.net,1433;Initial Catalog=coffeeclub;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=\"Active Directory Default\";");

        return new CoffeeClubContext(optionsBuilder.Options);
    }
}
