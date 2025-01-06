using DataMigration.Domain.Common;
using DataMigration.Domain.Enums;
using DataMigration.Domain.ValueObjects;

namespace DataMigration.Domain.Events;

public record UserCreatedEvent(
    Guid UserId,
    Email Email,
    string Name,
    bool IsWorkspaceUser,
    DateTime CreatedAt) : DomainEvent;

public record UserCreatedFromGoogleEvent(
    Guid UserId,
    Email Email,
    string GoogleId,
    string Name,
    string? Picture,
    string? Domain,
    bool IsWorkspaceUser,
    DateTime CreatedAt) : DomainEvent;

public record PasswordSetEvent(
    Guid UserId,
    string PasswordHash) : DomainEvent;

public record MfaEnabledEvent(
    Guid UserId, 
    string Secret) : DomainEvent;

public record MfaDisabledEvent(
    Guid UserId) : DomainEvent;

public record UserProfileUpdatedEvent(
    Guid UserId,
    string Name,
    string? Picture) : DomainEvent;

public record LoginSucceededEvent(
    Guid UserId,
    DateTime Timestamp) : DomainEvent;

public record LoginFailedEvent(
    Guid UserId,
    DateTime Timestamp) : DomainEvent;

public record UserLockedOutEvent(
    Guid UserId,
    DateTime LockoutEnd) : DomainEvent;

public record EmailVerifiedEvent(
    Guid UserId) : DomainEvent;

public record UserRoleAddedEvent(
    Guid UserId,
    UserRole Role) : DomainEvent;

public record UserRoleRemovedEvent(
    Guid UserId,
    UserRole Role) : DomainEvent; 