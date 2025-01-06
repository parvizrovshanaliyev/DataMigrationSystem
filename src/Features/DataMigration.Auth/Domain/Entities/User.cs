using DataMigration.Auth.Domain.Events;
using DataMigration.Common.Domain;

namespace DataMigration.Auth.Domain.Entities;

public class User : AggregateRoot
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Picture { get; private set; }
    public bool IsWorkspaceUser { get; private set; }
    public string? Domain { get; private set; }
    public string? GoogleId { get; private set; }
    public string? PasswordHash { get; private set; }
    public string? MfaSecret { get; private set; }
    public bool IsMfaEnabled { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public List<string> Roles { get; private set; } = new();
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    private User() { }

    public static User CreateLocal(
        string email,
        string name,
        string passwordHash)
    {
        var user = new User();
        user.Apply(new UserCreatedEvent(
            Guid.NewGuid(),
            email,
            name,
            false,
            DateTime.UtcNow
        ));
        user.Apply(new PasswordSetEvent(passwordHash));
        return user;
    }

    public static User CreateFromGoogle(
        string email,
        string googleId,
        string name,
        string? picture,
        string? domain)
    {
        var user = new User();
        user.Apply(new UserCreatedFromGoogleEvent(
            Guid.NewGuid(),
            email,
            googleId,
            name,
            picture,
            domain,
            true,
            DateTime.UtcNow
        ));
        return user;
    }

    public void EnableMfa(string secret)
    {
        if (IsMfaEnabled)
            throw new InvalidOperationException("MFA is already enabled");

        Apply(new MfaEnabledEvent(Id, secret));
    }

    public void DisableMfa()
    {
        if (!IsMfaEnabled)
            throw new InvalidOperationException("MFA is not enabled");

        Apply(new MfaDisabledEvent(Id));
    }

    public void UpdateProfile(string name, string? picture)
    {
        Apply(new UserProfileUpdatedEvent(Id, name, picture));
    }

    public void RecordLoginAttempt(bool successful)
    {
        if (successful)
        {
            Apply(new LoginSucceededEvent(Id, DateTime.UtcNow));
        }
        else
        {
            Apply(new LoginFailedEvent(Id, DateTime.UtcNow));
            if (FailedLoginAttempts >= 5)
            {
                Apply(new UserLockedOutEvent(Id, DateTime.UtcNow.AddMinutes(30)));
            }
        }
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                Id = e.UserId;
                Email = e.Email;
                Name = e.Name;
                IsWorkspaceUser = e.IsWorkspaceUser;
                CreatedAt = e.CreatedAt;
                break;

            case UserCreatedFromGoogleEvent e:
                Id = e.UserId;
                Email = e.Email;
                GoogleId = e.GoogleId;
                Name = e.Name;
                Picture = e.Picture;
                Domain = e.Domain;
                IsWorkspaceUser = e.IsWorkspaceUser;
                CreatedAt = e.CreatedAt;
                break;

            case PasswordSetEvent e:
                PasswordHash = e.PasswordHash;
                break;

            case MfaEnabledEvent e:
                IsMfaEnabled = true;
                MfaSecret = e.Secret;
                break;

            case MfaDisabledEvent:
                IsMfaEnabled = false;
                MfaSecret = null;
                break;

            case UserProfileUpdatedEvent e:
                Name = e.Name;
                Picture = e.Picture;
                break;

            case LoginSucceededEvent e:
                FailedLoginAttempts = 0;
                LockoutEnd = null;
                LastLoginAt = e.Timestamp;
                break;

            case LoginFailedEvent:
                FailedLoginAttempts++;
                break;

            case UserLockedOutEvent e:
                LockoutEnd = e.LockoutEnd;
                break;
        }
    }
} 