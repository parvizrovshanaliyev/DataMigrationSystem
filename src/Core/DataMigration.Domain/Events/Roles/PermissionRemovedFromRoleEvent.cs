using DataMigration.Domain.Common;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Events.Roles;

public sealed class PermissionRemovedFromRoleEvent : DomainEvent
{
    public Guid RoleId { get; }
    public Permission Permission { get; }

    public PermissionRemovedFromRoleEvent(Guid roleId, Permission permission)
    {
        RoleId = roleId;
        Permission = permission;
    }
} 