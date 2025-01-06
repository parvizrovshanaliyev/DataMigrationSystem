using DataMigration.Domain.Common;

namespace DataMigration.Domain.Events.Profile;

public sealed class UserCreatedFromGoogleEvent : DomainEvent
{
    public Guid UserId { get; }
    public string Email { get; }
    public string GoogleId { get; }
    public string Name { get; }
    public string? Picture { get; }
    public string? Domain { get; }
    public bool IsWorkspaceUser { get; }

    public UserCreatedFromGoogleEvent(
        Guid userId,
        string email,
        string googleId,
        string name,
        string? picture,
        string? domain,
        bool isWorkspaceUser) : base()
    {
        UserId = userId;
        Email = email;
        GoogleId = googleId;
        Name = name;
        Picture = picture;
        Domain = domain;
        IsWorkspaceUser = isWorkspaceUser;
    }
}