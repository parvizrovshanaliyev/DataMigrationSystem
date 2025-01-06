using System.Linq.Expressions;

namespace DataMigration.Domain.Common;

/// <summary>
/// Repository interface for aggregate roots only.
/// According to DDD principles, only Aggregate Roots should have repositories.
/// </summary>
/// <typeparam name="TAggregateRoot">The type of the aggregate root.</typeparam>
public interface IRepository<TAggregateRoot> where TAggregateRoot : AggregateRoot
{
    /// <summary>
    /// Retrieves an aggregate root by its identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the aggregate root.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The aggregate root if found; otherwise, null.</returns>
    Task<TAggregateRoot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all instances of the aggregate root.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of all aggregate roots.</returns>
    Task<IReadOnlyList<TAggregateRoot>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds aggregate roots based on a predicate.
    /// </summary>
    /// <param name="predicate">The predicate to filter aggregate roots.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A read-only list of matching aggregate roots.</returns>
    Task<IReadOnlyList<TAggregateRoot>> FindAsync(
        Expression<Func<TAggregateRoot, bool>> predicate, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new aggregate root to the repository.
    /// </summary>
    /// <param name="aggregateRoot">The aggregate root to add.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing aggregate root in the repository.
    /// </summary>
    /// <param name="aggregateRoot">The aggregate root to update.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task UpdateAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);

    /// <summary>
    /// Deletes an aggregate root from the repository.
    /// </summary>
    /// <param name="aggregateRoot">The aggregate root to delete.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task DeleteAsync(TAggregateRoot aggregateRoot, CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in this repository.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
} 