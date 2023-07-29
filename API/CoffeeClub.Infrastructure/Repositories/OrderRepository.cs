using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Models;
using CoffeeClub.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeeClub.Infrastructure.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(CoffeeClubContext context) : base(context)
    {
    }

    public Task<IEnumerable<Order>> GetForUser(Guid userId)
    {
        var all = _context.Set<Order>()
            .Include(x => x.User)
            .Where(x => x.User.Id == userId).AsEnumerable();
        return Task.FromResult(all);
    }
}
