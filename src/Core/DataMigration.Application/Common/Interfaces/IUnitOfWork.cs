using System.Threading;
using System.Threading.Tasks;

namespace DataMigration.Application.Common.Interfaces;

/// <summary>
/// Defines methods for managing database transactions
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Begins a new transaction
    /// </summary>
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Commits the current transaction
    /// </summary>
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Rolls back the current transaction
    /// </summary>
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Saves all changes made in this context to the database
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action within a transaction
    /// </summary>
    Task ExecuteInTransactionAsync(
        System.Func<Task> action,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the specified action within a transaction and returns a result
    /// </summary>
    Task<T> ExecuteInTransactionAsync<T>(
        System.Func<Task<T>> action,
        CancellationToken cancellationToken = default);
} 