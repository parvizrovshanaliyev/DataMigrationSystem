namespace DataMigration.Domain.ValueObjects;

public record Permission
{
    public string Resource { get; }
    public string Action { get; }
    public string Name { get; }

    public Permission(string permission)
    {
        var parts = permission.Split(':');
        if (parts.Length != 3)
            throw new ArgumentException("Invalid permission format");

        Resource = parts[0];
        Action = parts[1];
        Name = parts[2];
    }

    public override string ToString() => $"{Resource}:{Action}:{Name}";
}