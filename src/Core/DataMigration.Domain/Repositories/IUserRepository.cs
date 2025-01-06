using DataMigration.Domain.Entities;

namespace DataMigration.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByGoogleIdAsync(string googleId);
    Task<List<User>> GetAllAsync();
    Task<List<User>> GetByDomainAsync(string domain);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(User user);
} 