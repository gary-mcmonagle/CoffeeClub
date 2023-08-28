using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeeClub.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(CoffeeClubContext context) : base(context)
    {
    }

    public async Task<User?> GetAsync(string id, AuthProvider authProvider) =>
     await _context.Set<User>().FirstOrDefaultAsync(u => u.AuthId == id && u.AuthProvider == authProvider);

    public async Task<User?> GetOrCreateAsync(string id, AuthProvider authProvider, bool isWorker)
    {
        var user = await GetAsync(id, authProvider);
        if (user is null)
        {
            user = new User { AuthId = id, AuthProvider = authProvider, IsWorker = isWorker };
            await CreateAsync(user);
        }
        return user;
    }
}
