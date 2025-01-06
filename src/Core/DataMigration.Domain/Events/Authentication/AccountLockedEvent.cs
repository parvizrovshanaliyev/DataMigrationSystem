using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class AccountLockedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public int FailedAttempts { get; }
    public DateTime LockoutEnd { get; }
    public string? IpAddress { get; }

    public AccountLockedEvent(
        Guid userId,
        string username,
        int failedAttempts,
        DateTime lockoutEnd,
        string? ipAddress) : base()
    {
        UserId = userId;
        Username = username;
        FailedAttempts = failedAttempts;
        LockoutEnd = lockoutEnd;
        IpAddress = ipAddress;
    }
} 