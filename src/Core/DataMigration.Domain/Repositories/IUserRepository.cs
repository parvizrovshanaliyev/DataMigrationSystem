using DataMigration.Domain.Common;
using DataMigration.Domain.Entities;

namespace DataMigration.Domain.Repositories;

/// <summary>
/// Repository for User aggregate root.
/// Handles User entity and its child entities (LoginHistory, UserRole, UserGroup).
/// </summary>
public interface IUserRepository : IRepository<User>
{
    // User-specific queries
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> ExistsWithUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);

    // Login history queries (part of User aggregate)
    Task<IReadOnlyList<LoginHistory>> GetLoginHistoryAsync(Guid userId, int limit = 10, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LoginHistory>> GetFailedLoginAttemptsAsync(Guid userId, DateTime since, CancellationToken cancellationToken = default);
    Task<int> GetFailedLoginAttemptsCountAsync(Guid userId, DateTime since, CancellationToken cancellationToken = default);
    Task DeleteLoginHistoryOlderThanAsync(DateTime date, CancellationToken cancellationToken = default);
}