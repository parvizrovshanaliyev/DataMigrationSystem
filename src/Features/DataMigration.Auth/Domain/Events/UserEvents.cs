using DataMigration.Common.Domain;

namespace DataMigration.Auth.Domain.Events;

public record UserCreatedEvent(
    Guid UserId,
    string Email,
    string Name,
    bool IsWorkspaceUser,
    DateTime CreatedAt) : IDomainEvent;

public record UserCreatedFromGoogleEvent(
    Guid UserId,
    string Email,
    string GoogleId,
    string Name,
    string? Picture,
    string? Domain,
    bool IsWorkspaceUser,
    DateTime CreatedAt) : IDomainEvent;

public record PasswordSetEvent(string PasswordHash) : IDomainEvent;

public record MfaEnabledEvent(Guid UserId, string Secret) : IDomainEvent;

public record MfaDisabledEvent(Guid UserId) : IDomainEvent;

public record UserProfileUpdatedEvent(
    Guid UserId,
    string Name,
    string? Picture) : IDomainEvent;

public record LoginSucceededEvent(
    Guid UserId,
    DateTime Timestamp) : IDomainEvent;

public record LoginFailedEvent(
    Guid UserId,
    DateTime Timestamp) : IDomainEvent;

public record UserLockedOutEvent(
    Guid UserId,
    DateTime LockoutEnd) : IDomainEvent; 