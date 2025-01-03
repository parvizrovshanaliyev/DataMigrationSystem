using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class PasswordResetRequestedEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string ResetToken { get; }
    public DateTime ExpiresAt { get; }
    public string? IpAddress { get; }

    public PasswordResetRequestedEvent(User user, string resetToken, DateTime expiresAt, string? ipAddress = null)
    {
        UserId = user.Id;
        Email = user.Email;
        ResetToken = resetToken;
        ExpiresAt = expiresAt;
        IpAddress = ipAddress;
    }
} 