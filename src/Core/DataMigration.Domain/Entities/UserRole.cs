using DataMigration.Domain.Common;

namespace DataMigration.Domain.Entities;

public class UserRole : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid RoleId { get; private set; }
    public  string RoleName { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private UserRole() { } // For EF Core

    public static UserRole Create(Guid userId, Guid roleId, string roleName)
    {
        ArgumentException.ThrowIfNullOrEmpty(roleName);

        return new UserRole
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            RoleId = roleId,
            RoleName = roleName,
            AssignedAt = DateTime.UtcNow
        };
    }
}