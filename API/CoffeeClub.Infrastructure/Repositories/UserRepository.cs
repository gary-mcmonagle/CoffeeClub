using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;

namespace CoffeeClub.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(CoffeeClubContext context) : base(context)
    {
    }
}
