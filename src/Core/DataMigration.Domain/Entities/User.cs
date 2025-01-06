using DataMigration.Domain.Common;
using DataMigration.Domain.Events;
using DataMigration.Domain.Enums;
using DataMigration.Domain.Events.Authentication;
using DataMigration.Domain.Events.Groups;
using DataMigration.Domain.Events.Profile;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Entities;

public class User : AggregateRoot
{
    private const int MaxFailedLoginAttempts = 5;
    private const int LockoutDurationMinutes = 15;

    public Guid Id { get; private set; }
    public  string Email { get; private set; }
    public  string Username { get; private set; }
    public string? PasswordHash { get; private set; }
    public string? GoogleId { get; private set; }
    public  string Name { get; private set; }
    public string? Picture { get; private set; }
    public string? Domain { get; private set; }
    public bool IsWorkspaceUser { get; private set; }
    public bool IsMfaEnabled { get; private set; }
    public string? MfaSecret { get; private set; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutEnd { get; private set; }
    public DateTime LastSyncedAt { get; private set; }
    private readonly List<UserRole> _roles = new();
    private readonly List<UserGroup> _groups = new();

    public IReadOnlyList<UserRole> Roles => _roles.AsReadOnly();
    public IReadOnlyList<UserGroup> Groups => _groups.AsReadOnly();

    private User() { } // For EF Core

    public static User CreateLocal(string email, string username, string name, string hashedPassword)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(username);
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(hashedPassword);

        var user = new User();
        user.Apply(new UserCreatedEvent(Guid.NewGuid(), email, username, name));
        user.PasswordHash = hashedPassword;
        return user;
    }

    public static User CreateFromGoogle(GoogleUserInfo info)
    {
        ArgumentNullException.ThrowIfNull(info);

        var user = new User();
        user.Apply(new UserCreatedFromGoogleEvent(
            Guid.NewGuid(),
            info.Email,
            info.Id,
            info.Name,
            info.Picture,
            info.Domain,
            true));
        return user;
    }

    public void UpdateProfile(string name, string email, string? picture = null)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(email);

        Apply(new UserProfileUpdatedEvent(Id, name, email, picture));
    }

    public void EnableMfa(string secret)
    {
        ArgumentException.ThrowIfNullOrEmpty(secret);
        if (IsMfaEnabled)
            throw new InvalidOperationException("MFA is already enabled");

        Apply(new MfaEnabledEvent(Id, Username, secret));
    }

    public void DisableMfa(string? ipAddress)
    {
        if (!IsMfaEnabled)
            throw new InvalidOperationException("MFA is not enabled");

        Apply(new MfaDisabledEvent(Id, Username, ipAddress));
    }

    public void SyncGroups(IReadOnlyList<string> groups, IReadOnlyList<string> roles)
    {
        ArgumentNullException.ThrowIfNull(groups);
        ArgumentNullException.ThrowIfNull(roles);

        Apply(new UserGroupsSyncedEvent(Id, Email, groups, roles));
    }

    public void RecordLoginFailure(string? reason, string? ipAddress, string? userAgent)
    {
        FailedLoginAttempts++;

        Apply(new LoginFailedEvent(
            Id,
            Username,
            reason,
            ipAddress,
            userAgent,
            FailedLoginAttempts));

        if (FailedLoginAttempts >= MaxFailedLoginAttempts)
        {
            var lockoutEnd = DateTime.UtcNow.AddMinutes(LockoutDurationMinutes);
            Apply(new AccountLockedEvent(
                Id,
                Username,
                FailedLoginAttempts,
                lockoutEnd,
                ipAddress));
        }
    }

    public void RecordLoginSuccess(AuthenticationProvider provider, string? ipAddress, string? userAgent)
    {
        Apply(new LoginSucceededEvent(
            Id,
            Username,
            provider,
            ipAddress,
            userAgent,
            IsMfaEnabled));

        // Reset failed attempts on successful login
        if (FailedLoginAttempts > 0)
        {
            FailedLoginAttempts = 0;
            LockoutEnd = null;
        }
    }

    public void RecordLogout(string? ipAddress, string? userAgent)
    {
        Apply(new UserLoggedOutEvent(Id, Username, ipAddress, userAgent));
    }

    public bool IsLockedOut()
    {
        return LockoutEnd.HasValue && LockoutEnd.Value > DateTime.UtcNow;
    }

    protected override void When(IDomainEvent @event)
    {
        switch (@event)
        {
            case UserCreatedEvent e:
                Id = e.UserId;
                Email = e.Email;
                Username = e.Username;
                Name = e.Name;
                break;

            case UserCreatedFromGoogleEvent e:
                Id = e.UserId;
                Email = e.Email;
                GoogleId = e.GoogleId;
                Name = e.Name;
                Picture = e.Picture;
                Domain = e.Domain;
                IsWorkspaceUser = e.IsWorkspaceUser;
                LastSyncedAt = DateTime.UtcNow;
                break;

            case UserProfileUpdatedEvent e:
                Name = e.Name;
                Email = e.Email;
                if (e.Picture != null)
                    Picture = e.Picture;
                break;

            case MfaEnabledEvent e:
                IsMfaEnabled = true;
                MfaSecret = e.Secret;
                break;

            case MfaDisabledEvent _:
                IsMfaEnabled = false;
                MfaSecret = null;
                break;

            case UserGroupsSyncedEvent e:
                _groups.Clear();
                foreach (var group in e.Groups)
                {
                    _groups.Add(UserGroup.Create(Id, group, group));
                }
                LastSyncedAt = DateTime.UtcNow;
                break;

            case LoginFailedEvent e:
                FailedLoginAttempts = e.FailedAttempts;
                break;

            case AccountLockedEvent e:
                LockoutEnd = e.LockoutEnd;
                break;

            case LoginSucceededEvent _:
                FailedLoginAttempts = 0;
                LockoutEnd = null;
                break;
        }
    }
}