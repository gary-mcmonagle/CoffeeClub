using CoffeeClub.Domain.Dtos.Request;
using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<IEnumerable<Order>> GetForUser(Guid userId);
}
