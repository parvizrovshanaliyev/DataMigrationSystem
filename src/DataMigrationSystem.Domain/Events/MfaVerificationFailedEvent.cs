using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class MfaVerificationFailedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string Reason { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }

    public MfaVerificationFailedEvent(User user, string reason, string? ipAddress = null, string? userAgent = null)
    {
        UserId = user.Id;
        Email = user.Email;
        Reason = reason;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
} 