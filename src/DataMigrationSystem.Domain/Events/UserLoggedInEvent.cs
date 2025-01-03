using System;
using DataMigrationSystem.Domain.Entities;
using DataMigrationSystem.Domain.Enums;

namespace DataMigrationSystem.Domain.Events;

public class UserLoggedInEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public AuthProvider AuthProvider { get; }
    public bool IsMfaEnabled { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }

    public UserLoggedInEvent(User user, string? ipAddress = null, string? userAgent = null)
    {
        UserId = user.Id;
        Email = user.Email;
        AuthProvider = user.AuthProvider;
        IsMfaEnabled = user.IsMfaEnabled;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
} 