using System;
using DataMigration.Domain.Common;

namespace DataMigration.UserManagement.Domain.Events
{
    public class RoleCreatedEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public string Name { get; }

        public RoleCreatedEvent(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
    }

    public class RoleUpdatedEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public string Name { get; }

        public RoleUpdatedEvent(Guid roleId, string name)
        {
            RoleId = roleId;
            Name = name;
        }
    }

    public class RolePermissionAddedEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public Guid PermissionId { get; }

        public RolePermissionAddedEvent(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }

    public class RolePermissionRemovedEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public Guid PermissionId { get; }

        public RolePermissionRemovedEvent(Guid roleId, Guid permissionId)
        {
            RoleId = roleId;
            PermissionId = permissionId;
        }
    }

    public class RoleDefaultStatusChangedEvent : DomainEvent
    {
        public Guid RoleId { get; }
        public bool IsDefault { get; }

        public RoleDefaultStatusChangedEvent(Guid roleId, bool isDefault)
        {
            RoleId = roleId;
            IsDefault = isDefault;
        }
    }
} 