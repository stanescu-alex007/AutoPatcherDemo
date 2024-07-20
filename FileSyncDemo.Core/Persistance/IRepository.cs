using FileSyncDemo.Core.Entities;

namespace FileSyncDemo.Core.Persistance;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> FirstOrDefaultAsync();
    Task SaveChangesAsync();

}
