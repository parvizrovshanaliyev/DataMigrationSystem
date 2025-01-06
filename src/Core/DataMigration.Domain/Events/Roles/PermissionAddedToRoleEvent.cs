using DataMigration.Domain.Common;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Events.Roles;

public sealed class PermissionAddedToRoleEvent : DomainEvent
{
    public Guid RoleId { get; }
    public Permission Permission { get; }

    public PermissionAddedToRoleEvent(Guid roleId, Permission permission)
    {
        RoleId = roleId;
        Permission = permission;
    }
} 