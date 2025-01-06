using DataMigration.Domain.Common;

namespace DataMigration.Domain.Projects.Events;

public record ProjectCreatedEvent : IDomainEvent
{
    public Guid Id { get; } = Guid.NewGuid();
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
    public Guid ProjectId { get; }
    public string ProjectName { get; }
    public Guid OwnerId { get; }

    public ProjectCreatedEvent(Guid projectId, string projectName, Guid ownerId)
    {
        ProjectId = projectId;
        ProjectName = projectName;
        OwnerId = ownerId;
    }
} 