namespace DataMigration.Domain.Common;

public interface IRepository<TEntity> where TEntity : Entity
{
    IUnitOfWork UnitOfWork { get; }
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<List<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
} 