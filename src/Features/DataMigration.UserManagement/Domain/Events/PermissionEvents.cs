using System;
using DataMigration.Domain.Common;

namespace DataMigration.UserManagement.Domain.Events
{
    public class PermissionCreatedEvent : DomainEvent
    {
        public Guid PermissionId { get; }
        public string Name { get; }
        public string Resource { get; }
        public string Action { get; }

        public PermissionCreatedEvent(Guid permissionId, string name, string resource, string action)
        {
            PermissionId = permissionId;
            Name = name;
            Resource = resource;
            Action = action;
        }
    }

    public class PermissionUpdatedEvent : DomainEvent
    {
        public Guid PermissionId { get; }
        public string Name { get; }
        public string Resource { get; }
        public string Action { get; }

        public PermissionUpdatedEvent(Guid permissionId, string name, string resource, string action)
        {
            PermissionId = permissionId;
            Name = name;
            Resource = resource;
            Action = action;
        }
    }
} 