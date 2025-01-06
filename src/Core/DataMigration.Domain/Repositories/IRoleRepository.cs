using DataMigration.Domain.Common;
using DataMigration.Domain.Entities;

namespace DataMigration.Domain.Repositories;

/// <summary>
/// Repository for Role aggregate root.
/// Handles Role entity and its value objects (Permissions).
/// </summary>
public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);
}