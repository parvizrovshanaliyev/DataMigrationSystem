namespace DataMigration.Domain.Common;

public abstract class AggregateRoot : Entity
{
    protected void Apply(IDomainEvent @event)
    {
        When(@event);
        AddDomainEvent(@event);
    }

    protected abstract void When(IDomainEvent @event);
} 