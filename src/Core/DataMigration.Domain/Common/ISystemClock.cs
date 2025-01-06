namespace DataMigration.Domain.Common;

public interface ISystemClock
{
    DateTime Now { get; }
    DateTime UtcNow { get; }
} 