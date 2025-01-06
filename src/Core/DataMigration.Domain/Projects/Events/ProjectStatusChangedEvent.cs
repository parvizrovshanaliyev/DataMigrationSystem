using DataMigration.Domain.Common;

namespace DataMigration.Domain.Projects.Events;

public record ProjectStatusChangedEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Guid ProjectId { get; }
    public ProjectStatus NewStatus { get; }

    public ProjectStatusChangedEvent(Guid projectId, ProjectStatus newStatus)
    {
        ProjectId = projectId;
        NewStatus = newStatus;
    }
} 