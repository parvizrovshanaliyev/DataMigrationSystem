using System;
using DataMigrationSystem.Domain.Entities;

namespace DataMigrationSystem.Domain.Events;

public class ProjectCreatedEvent : DomainEvent
{
    public Guid ProjectId { get; }
    public string ProjectName { get; }
    public Guid CreatedById { get; }

    public ProjectCreatedEvent(Project project)
    {
        ProjectId = project.Id;
        ProjectName = project.Name;
        CreatedById = project.CreatedById;
    }
} 