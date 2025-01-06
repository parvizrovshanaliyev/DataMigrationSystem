using DataMigration.Domain.Common;
using DataMigration.Domain.Projects.Events;

namespace DataMigration.Domain.Projects;

public class Project : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ProjectStatus Status { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    private readonly List<ProjectUser> _users = new();
    private readonly Dictionary<string, string> _metadata = new();

    public IReadOnlyList<ProjectUser> Users => _users.AsReadOnly();
    public IReadOnlyDictionary<string, string> Metadata => _metadata;

    private Project() { }

    public static Project Create(string name, string description, Guid ownerId)
    {
        var project = new Project
        {
            Name = name,
            Description = description,
            OwnerId = ownerId,
            Status = ProjectStatus.Draft
        };

        project.AddDomainEvent(new ProjectCreatedEvent(project.Id, name, ownerId));
        return project;
    }

    public void UpdateStatus(ProjectStatus newStatus)
    {
        if (!CanTransitionTo(newStatus))
        {
            throw new InvalidOperationException($"Cannot transition from {Status} to {newStatus}");
        }

        Status = newStatus;
        if (newStatus == ProjectStatus.Completed)
        {
            CompletedAt = DateTime.UtcNow;
        }

        AddDomainEvent(new ProjectStatusChangedEvent(Id, Status));
    }

    public void AddUser(ProjectUser user)
    {
        if (_users.Any(u => u.UserId == user.UserId))
        {
            throw new InvalidOperationException("User already exists in project");
        }

        _users.Add(user);
        AddDomainEvent(new ProjectUserAddedEvent(Id, user.UserId, user.Role));
    }

    public void UpdateMetadata(string key, string value)
    {
        _metadata[key] = value;
        AddDomainEvent(new ProjectMetadataUpdatedEvent(Id, key, value));
    }

    private bool CanTransitionTo(ProjectStatus newStatus)
    {
        return Status switch
        {
            ProjectStatus.Draft => newStatus == ProjectStatus.Active,
            ProjectStatus.Active => newStatus == ProjectStatus.Completed || newStatus == ProjectStatus.Failed,
            _ => false
        };
    }
} 