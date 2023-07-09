using CoffeClub.Infrastructure;
using CoffeeBeanClub.Domain.Models;
using CoffeeClub.Domain.Repositories;

namespace CoffeeClub.Infrastructure.Repositories;

public class CoffeeBeanRepository : BaseRepository<CoffeeBean>, ICoffeeBeanRepository
{
    public CoffeeBeanRepository(CoffeeClubContext context) : base(context)
    {
    }
}