using System;

namespace DataMigration.Application.Common.Interfaces;

/// <summary>
/// Provides access to date and time operations
/// </summary>
public interface IDateTime
{
    /// <summary>
    /// Gets the current date and time in UTC
    /// </summary>
    DateTime Now { get; }

    /// <summary>
    /// Gets the current date in UTC
    /// </summary>
    DateTime Today { get; }

    /// <summary>
    /// Gets the current UTC offset
    /// </summary>
    TimeSpan UtcOffset { get; }
} 