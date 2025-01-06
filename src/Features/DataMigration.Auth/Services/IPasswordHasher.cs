namespace DataMigration.Auth.Services;

public interface IPasswordHasher
{
    Task<string> HashAsync(string password);
    Task<bool> ValidateAsync(string password, string hash);
} 