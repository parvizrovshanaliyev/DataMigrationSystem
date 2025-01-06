using DataMigration.Domain.Repositories;

namespace DataMigration.Domain.Contracts;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IRoleRepository Roles { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
} 