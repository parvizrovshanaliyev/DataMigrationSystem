using DataMigration.Domain.Common;

namespace DataMigration.Domain.Enums;

public class UserRole : Enumeration
{
    public static UserRole Admin = new(1, nameof(Admin));
    public static UserRole Manager = new(2, nameof(Manager));
    public static UserRole User = new(3, nameof(User));
    public static UserRole Guest = new(4, nameof(Guest));

    public UserRole(int id, string name) : base(id, name)
    {
    }
} 