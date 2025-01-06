using DataMigration.Domain.Common;
using DataMigration.Domain.Enums;

namespace DataMigration.Domain.Events.Authentication;

public sealed class LoginSucceededEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public AuthenticationProvider Provider { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }
    public bool IsMfaEnabled { get; }

    public LoginSucceededEvent(
        Guid userId,
        string username,
        AuthenticationProvider provider,
        string? ipAddress,
        string? userAgent,
        bool isMfaEnabled) : base()
    {
        UserId = userId;
        Username = username;
        Provider = provider;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        IsMfaEnabled = isMfaEnabled;
    }
} 