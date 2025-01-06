//using DataMigration.Domain.Contracts;

//namespace DataMigration.Infrastructure.Authentication.Services;

//public class PasswordHashingService : IPasswordHashingService
//{
//    public string HashPassword(string password)
//    {
//        return BCrypt.Net.BCrypt.HashPassword(password);
//    }

//    public bool VerifyPassword(string password, string hash)
//    {
//        return BCrypt.Net.BCrypt.Verify(password, hash);
//    }

//    public bool NeedsRehash(string hash)
//    {
//        return BCrypt.Net.BCrypt.PasswordNeedsRehash(hash);
//    }
//} 