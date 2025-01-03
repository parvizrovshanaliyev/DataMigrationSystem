using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class MfaEnabledEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string? IpAddress { get; }

    public MfaEnabledEvent(User user, string? ipAddress = null)
    {
        UserId = user.Id;
        Email = user.Email;
        IpAddress = ipAddress;
    }
} 