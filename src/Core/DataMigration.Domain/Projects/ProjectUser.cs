using DataMigration.Domain.Common;

namespace DataMigration.Domain.Projects;

public class ProjectUser : Entity
{
    public Guid ProjectId { get; private set; }
    public Guid UserId { get; private set; }
    public ProjectRole Role { get; private set; }
    public string? GoogleEmail { get; private set; }

    private ProjectUser() { }

    public static ProjectUser Create(Guid projectId, Guid userId, ProjectRole role, string? googleEmail = null)
    {
        return new ProjectUser
        {
            ProjectId = projectId,
            UserId = userId,
            Role = role,
            GoogleEmail = googleEmail
        };
    }

    public void UpdateRole(ProjectRole newRole)
    {
        Role = newRole;
    }
} 