using DataMigration.Domain.Common;
using DataMigration.Domain.Events;
using DataMigration.Domain.Enums;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Entities;

public class User : AggregateRoot
{
    public Guid Id { get; private set; }
    public Email Email { get; private set; } = null!;
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
    private readonly List<UserRole> _roles = new();
    public IReadOnlyList<UserRole> Roles => _roles.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }
    public bool IsEmailVerified { get; private set; }

    private User() { }

    public static User CreateLocal(Email email, string name, string passwordHash)
    {
        if (email is null)
            throw new ArgumentNullException(nameof(email));

        var user = new User();
        var userId = Guid.NewGuid();
        
        user.Apply(new UserCreatedEvent(
            userId,
            email,
            name,
            false,
            DateTime.UtcNow
        ));
        
        user.Apply(new PasswordSetEvent(userId, passwordHash));
        user._roles.Add(UserRole.User);
        return user;
    }

    public static User CreateFromGoogle(
        Email email,
        string googleId,
        string name,
        string? picture,
        string? domain)
    {
        if (email is null)
            throw new ArgumentNullException(nameof(email));

        var user = new User();
        var userId = Guid.NewGuid();
        
        user.Apply(new UserCreatedFromGoogleEvent(
            userId,
            email,
            googleId,
            name,
            picture,
            domain,
            true,
            DateTime.UtcNow
        ));
        
        user._roles.Add(UserRole.User);
        return user;
    }

    public void AddRole(UserRole role)
    {
        if (role is null)
            throw new ArgumentNullException(nameof(role));

        if (!_roles.Contains(role))
        {
            _roles.Add(role);
            Apply(new UserRoleAddedEvent(Id, role));
        }
    }

    public void RemoveRole(UserRole role)
    {
        if (role is null)
            throw new ArgumentNullException(nameof(role));

        if (_roles.Contains(role))
        {
            _roles.Remove(role);
            Apply(new UserRoleRemovedEvent(Id, role));
        }
    }

    public void EnableMfa(string secret)
    {
        if (string.IsNullOrWhiteSpace(secret))
            throw new ArgumentException("Secret cannot be empty", nameof(secret));

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
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty", nameof(name));

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

    public void VerifyEmail()
    {
        if (IsEmailVerified)
            throw new InvalidOperationException("Email is already verified");

        Apply(new EmailVerifiedEvent(Id));
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
                IsEmailVerified = true;
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

            case EmailVerifiedEvent:
                IsEmailVerified = true;
                break;
        }
    }
} 