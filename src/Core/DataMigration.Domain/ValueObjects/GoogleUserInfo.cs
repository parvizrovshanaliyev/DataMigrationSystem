namespace DataMigration.Domain.ValueObjects;

public record GoogleUserInfo
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string Name { get; init; }
    public string Picture { get; init; }
    public string Domain { get; init; }
    public bool EmailVerified { get; init; }
    public List<string> Groups { get; init; } = new();
}