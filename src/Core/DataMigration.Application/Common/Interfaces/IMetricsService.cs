using System;
using System.Collections.Generic;

namespace DataMigration.Application.Common.Interfaces;

/// <summary>
/// Provides metrics collection and reporting
/// </summary>
public interface IMetricsService
{
    /// <summary>
    /// Records a counter metric
    /// </summary>
    void IncrementCounter(string name, double value = 1, IDictionary<string, string>? tags = null);

    /// <summary>
    /// Records a gauge metric
    /// </summary>
    void RecordGauge(string name, double value, IDictionary<string, string>? tags = null);

    /// <summary>
    /// Records a histogram metric
    /// </summary>
    void RecordHistogram(string name, double value, IDictionary<string, string>? tags = null);

    /// <summary>
    /// Records the duration of an operation
    /// </summary>
    IDisposable BeginTimedOperation(string name, IDictionary<string, string>? tags = null);

    /// <summary>
    /// Records an event with optional metadata
    /// </summary>
    void RecordEvent(string name, IDictionary<string, string>? metadata = null);

    /// <summary>
    /// Gets the current value of a metric
    /// </summary>
    double GetMetricValue(string name, IDictionary<string, string>? tags = null);
} 