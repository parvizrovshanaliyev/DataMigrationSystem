namespace DataMigration.Domain.Contracts;

/// <summary>
/// Provides password hashing functionality for the domain
/// </summary>
public interface IPasswordHashingService
{
    /// <summary>
    /// Hashes a password using a secure algorithm
    /// </summary>
    string HashPassword(string password);

    /// <summary>
    /// Verifies if a password matches its hash
    /// </summary>
    bool VerifyPassword(string password, string hash);

    /// <summary>
    /// Checks if a password hash needs to be upgraded
    /// </summary>
    bool NeedsRehash(string hash);
} 