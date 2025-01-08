using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataMigration.Domain.Common;
using DataMigration.UserManagement.Domain.Entities;

namespace DataMigration.UserManagement.Domain.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Task<Role> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetDefaultRolesAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Role>> GetByPermissionAsync(Guid permissionId, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
    }
} 