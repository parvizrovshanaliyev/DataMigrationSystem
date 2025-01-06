using MediatR;

namespace DataMigration.Domain.Common;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _changes = new();
    public IReadOnlyCollection<IDomainEvent> Changes => _changes.AsReadOnly();

    protected void Apply(IDomainEvent @event)
    {
        When(@event);
        _changes.Add(@event);
        AddDomainEvent(@event);
    }

    protected abstract void When(IDomainEvent @event);

    public void ClearChanges()
    {
        _changes.Clear();
    }
} 