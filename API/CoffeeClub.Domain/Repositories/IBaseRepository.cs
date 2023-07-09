using CoffeeClub.Domain.Models;

namespace CoffeeClub.Domain.Repositories;

public interface IBaseRepository<T> where T : BaseModel
{
    Task<T> CreateAsync(T model);
    Task<T?> GetAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> UpdateAsync(T model);
    Task DeleteAsync(Guid id);
}
