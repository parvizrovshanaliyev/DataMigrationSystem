using System;
using System.Collections.Generic;
using DataMigration.Domain.Common;
using DataMigration.UserManagement.Domain.Events;
using DataMigration.UserManagement.Domain.ValueObjects;

namespace DataMigration.UserManagement.Domain.Entities
{
    public class User : AggregateRoot
    {
        private const int MaxFailedLoginAttempts = 5;
        private const int LockoutDurationMinutes = 15;

        private readonly List<Role> _roles = new();
        private readonly List<LoginHistory> _loginHistory = new();

        public Email Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public string Salt { get; private set; }
        public string GoogleId { get; private set; }
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string Domain { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsEmailVerified { get; private set; }
        public bool IsMfaEnabled { get; private set; }
        public string MfaSecret { get; private set; }
        public int FailedLoginAttempts { get; private set; }
        public DateTime? LockoutEnd { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastLoginAt { get; private set; }
        public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();
        public IReadOnlyCollection<LoginHistory> LoginHistory => _loginHistory.AsReadOnly();

        private User() { } // For EF Core

        public static User Create(string email, string username, string name)
        {
            Guard.AgainstEmptyString(email, nameof(email));
            Guard.AgainstEmptyString(username, nameof(username));
            Guard.AgainstEmptyString(name, nameof(name));

            var user = new User();
            user.Apply(new UserCreatedEvent(Guid.NewGuid(), email, username, name));
            return user;
        }

        public static User CreateWithPassword(string email, string username, string name, string passwordHash, string salt)
        {
            var user = Create(email, username, name);
            user.PasswordHash = passwordHash;
            user.Salt = salt;
            return user;
        }

        public static User CreateWithGoogle(string email, string username, string name, string googleId, string picture, string domain)
        {
            var user = Create(email, username, name);
            user.GoogleId = googleId;
            user.Picture = picture;
            user.Domain = domain;
            user.IsEmailVerified = true; // Google emails are pre-verified
            return user;
        }

        public void AddRole(Role role)
        {
            Guard.AgainstNull(role, nameof(role));
            
            if (!_roles.Contains(role))
            {
                Apply(new UserRoleAddedEvent(Id, role.Id));
                _roles.Add(role);
            }
        }

        public void RemoveRole(Role role)
        {
            Guard.AgainstNull(role, nameof(role));
            
            if (_roles.Contains(role))
            {
                Apply(new UserRoleRemovedEvent(Id, role.Id));
                _roles.Remove(role);
            }
        }

        public void UpdateEmail(string newEmail)
        {
            Guard.AgainstEmptyString(newEmail, nameof(newEmail));
            
            var email = Email.Create(newEmail);
            if (Email != email)
            {
                Apply(new UserEmailUpdatedEvent(Id, newEmail));
            }
        }

        public void VerifyEmail()
        {
            if (!IsEmailVerified)
            {
                Apply(new UserEmailVerifiedEvent(Id));
            }
        }

        public void Deactivate()
        {
            if (IsActive)
            {
                Apply(new UserDeactivatedEvent(Id));
            }
        }

        public void Activate()
        {
            if (!IsActive)
            {
                Apply(new UserActivatedEvent(Id));
            }
        }

        public void RecordLogin(string ipAddress = null, string userAgent = null)
        {
            if (IsLockedOut())
            {
                throw new InvalidOperationException("Account is locked out");
            }

            Apply(new UserLoggedInEvent(Id, ipAddress, userAgent));
            ResetFailedLoginAttempts();
        }

        public void RecordFailedLogin(string reason)
        {
            FailedLoginAttempts++;
            
            if (FailedLoginAttempts >= MaxFailedLoginAttempts)
            {
                LockoutEnd = DateTime.UtcNow.AddMinutes(LockoutDurationMinutes);
            }

            var loginHistory = LoginHistory.Create(DateTime.UtcNow);
            loginHistory.MarkAsFailed(reason);
            _loginHistory.Add(loginHistory);
        }

        public bool IsLockedOut()
        {
            if (!LockoutEnd.HasValue) return false;
            return LockoutEnd.Value > DateTime.UtcNow;
        }

        private void ResetFailedLoginAttempts()
        {
            FailedLoginAttempts = 0;
            LockoutEnd = null;
        }

        public void EnableMfa(string secret)
        {
            Guard.AgainstEmptyString(secret, nameof(secret));
            
            if (!IsMfaEnabled)
            {
                Apply(new UserMfaEnabledEvent(Id, secret));
            }
        }

        public void DisableMfa()
        {
            if (IsMfaEnabled)
            {
                Apply(new UserMfaDisabledEvent(Id));
            }
        }

        public void UpdatePassword(string newPasswordHash, string newSalt)
        {
            Guard.AgainstEmptyString(newPasswordHash, nameof(newPasswordHash));
            Guard.AgainstEmptyString(newSalt, nameof(newSalt));

            Apply(new UserPasswordUpdatedEvent(Id));
            PasswordHash = newPasswordHash;
            Salt = newSalt;
        }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case UserCreatedEvent e:
                    Id = e.UserId;
                    Email = Email.Create(e.Email);
                    Username = e.Username;
                    Name = e.Name;
                    IsActive = true;
                    IsEmailVerified = false;
                    CreatedAt = DateTime.UtcNow;
                    break;

                case UserRoleAddedEvent:
                    // Role is already added in AddRole method
                    break;

                case UserRoleRemovedEvent:
                    // Role is already removed in RemoveRole method
                    break;

                case UserEmailUpdatedEvent e:
                    Email = Email.Create(e.NewEmail);
                    IsEmailVerified = false;
                    break;

                case UserEmailVerifiedEvent:
                    IsEmailVerified = true;
                    break;

                case UserDeactivatedEvent:
                    IsActive = false;
                    break;

                case UserActivatedEvent:
                    IsActive = true;
                    break;

                case UserLoggedInEvent e:
                    LastLoginAt = DateTime.UtcNow;
                    _loginHistory.Add(LoginHistory.Create(DateTime.UtcNow, e.IpAddress, e.UserAgent));
                    break;

                case UserPasswordUpdatedEvent:
                    // Password hash and salt are set directly in UpdatePassword method
                    break;

                case UserMfaEnabledEvent e:
                    IsMfaEnabled = true;
                    MfaSecret = e.Secret;
                    break;

                case UserMfaDisabledEvent:
                    IsMfaEnabled = false;
                    MfaSecret = null;
                    break;
            }
        }
    }
} 