using System;
using System.Threading.Tasks;

namespace DataMigration.Application.Common.Interfaces;

/// <summary>
/// Provides caching operations
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Gets a cached item by key
    /// </summary>
    Task<T?> GetAsync<T>(string key) where T : class;

    /// <summary>
    /// Sets a cached item with the specified key and expiration
    /// </summary>
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null) where T : class;

    /// <summary>
    /// Removes a cached item by key
    /// </summary>
    Task RemoveAsync(string key);

    /// <summary>
    /// Checks if a cached item exists
    /// </summary>
    Task<bool> ExistsAsync(string key);

    /// <summary>
    /// Gets a cached item by key or creates it using the factory if it doesn't exist
    /// </summary>
    Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null) where T : class;

    /// <summary>
    /// Removes all cached items matching the specified pattern
    /// </summary>
    Task RemoveByPatternAsync(string pattern);
} 