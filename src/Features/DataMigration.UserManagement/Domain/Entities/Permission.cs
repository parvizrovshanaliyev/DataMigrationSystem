using System;
using DataMigration.Domain.Common;

namespace DataMigration.UserManagement.Domain.Entities
{
    public class Permission : Entity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Resource { get; private set; }
        public string Action { get; private set; }

        private Permission() { } // For EF Core

        public static Permission Create(string name, string description, string resource, string action)
        {
            Guard.AgainstEmptyString(name, nameof(name));
            Guard.AgainstEmptyString(description, nameof(description));
            Guard.AgainstEmptyString(resource, nameof(resource));
            Guard.AgainstEmptyString(action, nameof(action));

            return new Permission
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                Resource = resource,
                Action = action
            };
        }

        public void UpdateName(string newName)
        {
            Guard.AgainstEmptyString(newName, nameof(newName));
            Name = newName;
        }

        public void UpdateDescription(string newDescription)
        {
            Guard.AgainstEmptyString(newDescription, nameof(newDescription));
            Description = newDescription;
        }

        public void UpdateResource(string newResource)
        {
            Guard.AgainstEmptyString(newResource, nameof(newResource));
            Resource = newResource;
        }

        public void UpdateAction(string newAction)
        {
            Guard.AgainstEmptyString(newAction, nameof(newAction));
            Action = newAction;
        }
    }
} 