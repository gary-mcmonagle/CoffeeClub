using CoffeClub.Infrastructure;
using CoffeeClub.Domain.Repositories;

namespace CoffeeClub.Infrastructure.Repositories;

public abstract class BaseRepository<T> : Domain.Repositories.IBaseRepository<T> where T : Domain.Models.BaseModel
{
    private readonly CoffeeClubContext _context;

    public BaseRepository(CoffeeClubContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T model)
    {
        await _context.Set<T>().AddAsync(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task<T?> GetAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        var all = _context.Set<T>().AsEnumerable();
        return Task.FromResult(all);
    }

    public async Task<T> UpdateAsync(T model)
    {
        _context.Set<T>().Update(model);
        await _context.SaveChangesAsync();
        return model;
    }

    public async Task DeleteAsync(Guid id)
    {
        var model = await GetAsync(id);
        _context.Set<T>().Remove(model);
        await _context.SaveChangesAsync();
    }
}