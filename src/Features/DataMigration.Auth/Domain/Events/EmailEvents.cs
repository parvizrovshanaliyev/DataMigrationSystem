using DataMigration.Common.Domain;

namespace DataMigration.Auth.Domain.Events;

public record EmailVerifiedEvent(Guid UserId) : IDomainEvent; 