using DataMigration.Domain.Common;
using DataMigration.Domain.Enums;

namespace DataMigration.Domain.Entities;

public class LoginHistory : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public  string Username { get; private set; }
    public AuthenticationProvider Provider { get; private set; }
    public bool IsSuccessful { get; private set; }
    public string? FailureReason { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private LoginHistory() { } // For EF Core

    public static LoginHistory CreateSuccess(
        Guid userId,
        string username,
        AuthenticationProvider provider,
        string? ipAddress,
        string? userAgent)
    {
        ArgumentException.ThrowIfNullOrEmpty(username);

        return new LoginHistory
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Username = username,
            Provider = provider,
            IsSuccessful = true,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static LoginHistory CreateFailure(
        Guid userId,
        string username,
        string? failureReason,
        string? ipAddress,
        string? userAgent)
    {
        ArgumentException.ThrowIfNullOrEmpty(username);

        return new LoginHistory
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Username = username,
            Provider = AuthenticationProvider.Local,
            IsSuccessful = false,
            FailureReason = failureReason,
            IpAddress = ipAddress,
            UserAgent = userAgent,
            CreatedAt = DateTime.UtcNow
        };
    }
}