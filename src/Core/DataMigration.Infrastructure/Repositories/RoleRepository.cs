//using DataMigration.Domain.Entities;
//using DataMigration.Domain.Repositories;
//using DataMigration.Infrastructure.Persistence.Context;
//using DataMigration.Infrastructure.Repositories.Base;
//using Microsoft.EntityFrameworkCore;

//namespace DataMigration.Infrastructure.Repositories;

///// <summary>
///// Repository implementation for the Role aggregate root.
///// </summary>
//public class RoleRepository : Repository<Role>, IRoleRepository
//{
//    /// <summary>
//    /// Initializes a new instance of the RoleRepository.
//    /// </summary>
//    /// <param name="context">The database context.</param>
//    public RoleRepository(ApplicationDbContext context) : base(context)
//    {
//    }

//    /// <inheritdoc/>
//    public override async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
//    {
//        return await DbSet
//            .Include(r => r.UserRoles)
//            .ThenInclude(ur => ur.User)
//            .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted, cancellationToken);
//    }

//    /// <summary>
//    /// Gets a role by its name.
//    /// </summary>
//    /// <param name="name">The name of the role to search for.</param>
//    /// <param name="cancellationToken">The cancellation token.</param>
//    /// <returns>The role if found; otherwise, null.</returns>
//    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
//    {
//        return await DbSet
//            .Include(r => r.UserRoles)
//            .ThenInclude(ur => ur.User)
//            .FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted, cancellationToken);
//    }

//    public Task<IReadOnlyList<Role>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    public Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
//    {
//        throw new NotImplementedException();
//    }

//    /// <inheritdoc/>
//    public override async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default)
//    {
//        return await DbSet
//            .Include(r => r.UserRoles)
//            .ThenInclude(ur => ur.User)
//            .Where(r => !r.IsDeleted)
//            .ToListAsync(cancellationToken);
//    }
//} 