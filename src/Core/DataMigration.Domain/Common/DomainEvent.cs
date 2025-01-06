namespace DataMigration.Domain.Common;

public abstract class DomainEvent : IDomainEvent
{
    private static ISystemClock _clock = new DefaultSystemClock();

    public Guid Id { get; }
    public DateTime OccurredOn { get; }

    protected DomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = _clock.UtcNow;
    }

    public static void SetClock(ISystemClock clock)
    {
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }
}

internal class DefaultSystemClock : ISystemClock
{
    public DateTime Now => DateTime.Now;
    public DateTime UtcNow => DateTime.UtcNow;
} 