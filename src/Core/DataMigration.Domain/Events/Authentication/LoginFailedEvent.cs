using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class LoginFailedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public string? Reason { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }
    public int FailedAttempts { get; }

    public LoginFailedEvent(
        Guid userId,
        string username,
        string? reason,
        string? ipAddress,
        string? userAgent,
        int failedAttempts) : base()
    {
        UserId = userId;
        Username = username;
        Reason = reason;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        FailedAttempts = failedAttempts;
    }
} 