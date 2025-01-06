using DataMigration.Domain.Contracts;
using DataMigration.Domain.Repositories;
using DataMigration.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataMigration.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private IDbContextTransaction _currentTransaction;
    private bool _disposed;

    public IUserRepository Users { get; }
    public IRoleRepository Roles { get; }

    public UnitOfWork(
        ApplicationDbContext context,
        IUserRepository userRepository,
        IRoleRepository roleRepository)
    {
        _context = context;
        Users = userRepository;
        Roles = roleRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction = await _context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await SaveChangesAsync(cancellationToken);

            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync(cancellationToken);
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _context.Dispose();
            _currentTransaction?.Dispose();
        }
        _disposed = true;
    }
} 