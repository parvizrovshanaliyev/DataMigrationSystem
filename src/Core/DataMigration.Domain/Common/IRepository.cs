using System.Linq.Expressions;

namespace DataMigration.Domain.Common;

/// <summary>
/// Repository interface for aggregate roots only.
/// According to DDD principles, only Aggregate Roots should have repositories.
/// </summary>
public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    Task<TAggregateRoot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TAggregateRoot>> FindAsync(Expression<Func<TAggregateRoot, bool>> predicate, CancellationToken cancellationToken = default);
    Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    Task UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 