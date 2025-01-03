using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class UserEmailVerifiedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }

    public UserEmailVerifiedEvent(User user)
    {
        UserId = user.Id;
        Email = user.Email;
    }
} 