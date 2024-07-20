using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using Microsoft.EntityFrameworkCore;

namespace FileSyncDemo.Infrastructure.Persistance;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected ApplicationDbContext Context { get; set; }
    private readonly DbSet<TEntity> _entities;

    public Repository(ApplicationDbContext context)
    {
        Context = context;
        _entities = context.Set<TEntity>();
    }


    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _entities.AddAsync(entity);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity)
        => await Task.FromResult(_entities.Remove(entity));

    public async Task<TEntity?> GetByIdAsync(Guid id)
        => await _entities.FindAsync(id);

    public async Task SaveChangesAsync()
        => await Context.SaveChangesAsync();

    public async Task<TEntity?> FirstOrDefaultAsync()
        => await _entities.OrderBy(e => e.Id).FirstOrDefaultAsync();

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await _entities.ToListAsync();
}
