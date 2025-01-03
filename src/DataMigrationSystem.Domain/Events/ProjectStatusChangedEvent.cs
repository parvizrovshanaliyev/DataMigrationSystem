using System;
using DataMigrationSystem.Domain.Entities;
using DataMigrationSystem.Domain.Enums;

namespace DataMigrationSystem.Domain.Events;

public class ProjectStatusChangedEvent : DomainEvent
{
    public Guid ProjectId { get; }
    public string ProjectName { get; }
    public ProjectStatus OldStatus { get; }
    public ProjectStatus NewStatus { get; }

    public ProjectStatusChangedEvent(Project project, ProjectStatus oldStatus, ProjectStatus newStatus)
    {
        ProjectId = project.Id;
        ProjectName = project.Name;
        OldStatus = oldStatus;
        NewStatus = newStatus;
    }
} 