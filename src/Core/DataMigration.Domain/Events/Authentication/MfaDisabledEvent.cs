using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class MfaDisabledEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public string? IpAddress { get; }

    public MfaDisabledEvent(Guid userId, string username, string? ipAddress) : base()
    {
        UserId = userId;
        Username = username;
        IpAddress = ipAddress;
    }
} 