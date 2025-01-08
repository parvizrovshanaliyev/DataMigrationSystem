using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataMigration.Domain.Common;
using DataMigration.UserManagement.Domain.Entities;

namespace DataMigration.UserManagement.Domain.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {
        Task<Permission> GetByNameAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByResourceAsync(string resource, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByActionAsync(string action, CancellationToken cancellationToken = default);
        Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken = default);
        Task<IEnumerable<Permission>> GetByResourceAndActionAsync(string resource, string action, CancellationToken cancellationToken = default);
    }
} 