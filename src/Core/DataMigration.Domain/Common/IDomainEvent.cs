using MediatR;

namespace DataMigration.Domain.Common;

public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
    Guid EventId { get; }
} 