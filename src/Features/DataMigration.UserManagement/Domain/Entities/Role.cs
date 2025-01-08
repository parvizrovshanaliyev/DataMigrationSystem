using System;
using System.Collections.Generic;
using DataMigration.Domain.Common;
using DataMigration.UserManagement.Domain.Events;

namespace DataMigration.UserManagement.Domain.Entities
{
    public class Role : AggregateRoot
    {
        private readonly List<Permission> _permissions = new();

        public string Name { get; private set; }
        public string Description { get; private set; }
        public bool IsDefault { get; private set; }
        public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

        private Role() { } // For EF Core

        public static Role Create(string name, string description, bool isDefault = false)
        {
            Guard.AgainstEmptyString(name, nameof(name));
            Guard.AgainstEmptyString(description, nameof(description));

            var role = new Role();
            role.Apply(new RoleCreatedEvent(Guid.NewGuid(), name));
            role.Description = description;
            role.IsDefault = isDefault;
            return role;
        }

        public void UpdateName(string newName)
        {
            Guard.AgainstEmptyString(newName, nameof(newName));

            if (Name != newName)
            {
                Apply(new RoleUpdatedEvent(Id, newName));
            }
        }

        public void UpdateDescription(string newDescription)
        {
            Guard.AgainstEmptyString(newDescription, nameof(newDescription));

            if (Description != newDescription)
            {
                Description = newDescription;
                Apply(new RoleUpdatedEvent(Id, Name));
            }
        }

        public void AddPermission(Permission permission)
        {
            Guard.AgainstNull(permission, nameof(permission));

            if (!_permissions.Contains(permission))
            {
                Apply(new RolePermissionAddedEvent(Id, permission.Id));
                _permissions.Add(permission); // Add permission immediately as it's a reference
            }
        }

        public void RemovePermission(Permission permission)
        {
            Guard.AgainstNull(permission, nameof(permission));

            if (_permissions.Contains(permission))
            {
                Apply(new RolePermissionRemovedEvent(Id, permission.Id));
                _permissions.Remove(permission); // Remove permission immediately as it's a reference
            }
        }

        public void SetDefault(bool isDefault)
        {
            if (IsDefault != isDefault)
            {
                Apply(new RoleDefaultStatusChangedEvent(Id, isDefault));
            }
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case RoleCreatedEvent e:
                    Id = e.RoleId;
                    Name = e.Name;
                    break;

                case RoleUpdatedEvent e:
                    Name = e.Name;
                    break;

                case RolePermissionAddedEvent:
                    // Permission is already added in AddPermission method
                    break;

                case RolePermissionRemovedEvent:
                    // Permission is already removed in RemovePermission method
                    break;

                case RoleDefaultStatusChangedEvent e:
                    IsDefault = e.IsDefault;
                    break;
            }
        }
    }
} 