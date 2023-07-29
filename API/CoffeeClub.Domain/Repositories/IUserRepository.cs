using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetAsync(string id, AuthProvider authProvider);
    Task<User?> GetOrCreateAsync(string id, AuthProvider authProvider);
}
