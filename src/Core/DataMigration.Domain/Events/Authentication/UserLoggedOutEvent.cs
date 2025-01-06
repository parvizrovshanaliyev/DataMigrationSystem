using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Authentication;

public sealed class UserLoggedOutEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Username { get; }
    public string? IpAddress { get; }
    public string? UserAgent { get; }

    public UserLoggedOutEvent(
        Guid userId,
        string username,
        string? ipAddress,
        string? userAgent) : base()
    {
        UserId = userId;
        Username = username;
        IpAddress = ipAddress;
        UserAgent = userAgent;
    }
}