using CoffeeClub.Domain.Enumerations;

namespace CoffeeClub.Core.Api.Services;

public interface IOrderDispatchService
{
    Task UpdateOrder(Guid orderId, OrderStatus orderStatus);
}
