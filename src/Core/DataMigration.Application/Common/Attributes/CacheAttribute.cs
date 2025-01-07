using System;

namespace DataMigration.Application.Common.Attributes;

/// <summary>
/// Marks a request as cacheable with the specified options
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class CacheAttribute : Attribute
{
    /// <summary>
    /// Gets the cache duration in seconds
    /// </summary>
    public int DurationInSeconds { get; }

    /// <summary>
    /// Gets or sets the cache key prefix
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// Gets or sets whether to vary the cache by user
    /// </summary>
    public bool VaryByUser { get; set; } = true;

    /// <summary>
    /// Initializes a new instance of the CacheAttribute class
    /// </summary>
    /// <param name="durationInSeconds">The duration to cache the response for in seconds</param>
    public CacheAttribute(int durationInSeconds)
    {
        DurationInSeconds = durationInSeconds;
    }
} 