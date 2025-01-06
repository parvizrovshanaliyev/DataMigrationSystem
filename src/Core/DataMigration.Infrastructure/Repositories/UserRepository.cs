using DataMigration.Domain.Entities;
using DataMigration.Domain.Repositories;
using DataMigration.Infrastructure.Persistence.Context;
using DataMigration.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace DataMigration.Infrastructure.Repositories;

/// <summary>
/// Repository implementation for the User aggregate root.
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the UserRepository.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }

    /// <inheritdoc/>
    public override async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, cancellationToken);
    }

    /// <summary>
    /// Gets a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, cancellationToken);
    }

    /// <summary>
    /// Gets a user by their Google ID.
    /// </summary>
    /// <param name="googleId">The Google ID to search for.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user if found; otherwise, null.</returns>
    public async Task<User?> GetByGoogleIdAsync(string googleId, CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.GoogleId == googleId && !u.IsDeleted, cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task<IReadOnlyList<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await DbSet
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(u => u.UserGroups)
            .Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);
    }
} 