using System.Threading.Tasks;

namespace DataMigrationSystem.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
} 