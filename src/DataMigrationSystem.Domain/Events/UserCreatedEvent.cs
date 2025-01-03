using System;
using DataMigrationSystem.Domain.Entities;
using DataMigrationSystem.Domain.Enums;

namespace DataMigrationSystem.Domain.Events;

public class UserCreatedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public AuthProvider AuthProvider { get; }

    public UserCreatedEvent(User user)
    {
        UserId = user.Id;
        Email = user.Email;
        AuthProvider = user.AuthProvider;
    }
} 