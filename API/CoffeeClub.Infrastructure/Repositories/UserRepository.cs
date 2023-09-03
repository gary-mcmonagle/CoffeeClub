using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Enumerations;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using CoffeeClub.Infrastructure.Dapper;
using Microsoft.EntityFrameworkCore;
using Dapper;

namespace CoffeeClub.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly DapperContext _dapperContext;
    public UserRepository(CoffeeClubContext context, DapperContext dapperContext) : base(context)
    {
        _dapperContext = dapperContext;
    }

    public async Task<User?> GetAsync(string id, AuthProvider authProvider) =>
     await _context.Set<User>().FirstOrDefaultAsync(u => u.AuthId == id && u.AuthProvider == authProvider);

    public async Task<User?> GetOrCreateAsync(string id, AuthProvider authProvider, bool isWorker)
    {
        using (var connection = _dapperContext.CreateConnection())
        {
            var duser = await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE AuthId = @AuthId AND AuthProvider = @AuthProvider",
                 new { AuthId = id, AuthProvider = authProvider });
            if (duser is not null)
            {
                return duser;
            }
            else
            {
                await connection.ExecuteAsync(
                    "INSERT INTO Users (AuthId, AuthProvider, IsWorker) VALUES (@AuthId, @AuthProvider, @IsWorker)",
                    new { AuthId = id, AuthProvider = authProvider, IsWorker = isWorker });
                return await connection.QueryFirstOrDefaultAsync<User>(
                    "SELECT * FROM Users WHERE AuthId = @AuthId AND AuthProvider = @AuthProvider",
                    new { AuthId = id, AuthProvider = authProvider });
            }
        }
    }

    public async Task<Guid?> GetUserId(string authId, AuthProvider authProvider, bool isWorker) =>
        (await GetOrCreateAsync(authId, authProvider, isWorker))?.Id;
}
