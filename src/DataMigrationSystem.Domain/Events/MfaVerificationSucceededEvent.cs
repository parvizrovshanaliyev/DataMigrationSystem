using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class MfaVerificationSucceededEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }

    public MfaVerificationSucceededEvent(User user, string? ipAddress = null, string? userAgent = null)
    {
        UserId = user.Id;
        Email = user.Email;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
} 