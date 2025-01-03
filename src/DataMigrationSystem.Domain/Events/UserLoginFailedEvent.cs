using System;

namespace DataMigrationSystem.Domain.Events;

public class UserLoginFailedEvent : DomainEvent
{
    public string Email { get; }
    public string Reason { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }

    public UserLoginFailedEvent(string email, string reason, string? ipAddress = null, string? userAgent = null)
    {
        Email = email;
        Reason = reason;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
} 