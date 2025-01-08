using System;
using DataMigration.Domain.Common;

namespace DataMigration.UserManagement.Domain.Events
{
    public class UserCreatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string Username { get; }
        public string Name { get; }

        public UserCreatedEvent(Guid userId, string email, string username, string name)
        {
            UserId = userId;
            Email = email;
            Username = username;
            Name = name;
        }
    }

    public class UserEmailUpdatedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string NewEmail { get; }

        public UserEmailUpdatedEvent(Guid userId, string newEmail)
        {
            UserId = userId;
            NewEmail = newEmail;
        }
    }

    public class UserEmailVerifiedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserEmailVerifiedEvent(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UserDeactivatedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserDeactivatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UserActivatedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserActivatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UserLoggedInEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string IpAddress { get; }
        public string UserAgent { get; }

        public UserLoggedInEvent(Guid userId, string ipAddress = null, string userAgent = null)
        {
            UserId = userId;
            IpAddress = ipAddress;
            UserAgent = userAgent;
        }
    }

    public class UserPasswordUpdatedEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserPasswordUpdatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }

    public class UserRoleAddedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public Guid RoleId { get; }

        public UserRoleAddedEvent(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }

    public class UserRoleRemovedEvent : DomainEvent
    {
        public Guid UserId { get; }
        public Guid RoleId { get; }

        public UserRoleRemovedEvent(Guid userId, Guid roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }
    }

    public class UserMfaEnabledEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Secret { get; }

        public UserMfaEnabledEvent(Guid userId, string secret)
        {
            UserId = userId;
            Secret = secret;
        }
    }

    public class UserMfaDisabledEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserMfaDisabledEvent(Guid userId)
        {
            UserId = userId;
        }
    }
} 