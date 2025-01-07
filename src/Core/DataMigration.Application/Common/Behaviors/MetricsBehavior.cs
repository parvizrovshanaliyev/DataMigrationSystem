using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using DataMigration.Application.Common.Interfaces;

namespace DataMigration.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for collecting request metrics
/// </summary>
public class MetricsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IMetricsService _metrics;
    private readonly ICurrentUser _currentUser;

    public MetricsBehavior(
        IMetricsService metrics,
        ICurrentUser currentUser)
    {
        _metrics = metrics;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var tags = new Dictionary<string, string>
        {
            ["request_type"] = requestName,
            ["user_id"] = _currentUser.Id?.ToString() ?? "anonymous"
        };

        // Increment request counter
        _metrics.IncrementCounter("requests_total", 1, tags);

        var sw = Stopwatch.StartNew();
        try
        {
            var response = await next();

            // Record success metrics
            sw.Stop();
            _metrics.RecordHistogram(
                "request_duration_seconds",
                sw.Elapsed.TotalSeconds,
                tags);

            _metrics.IncrementCounter(
                "requests_succeeded_total",
                1,
                tags);

            return response;
        }
        catch (Exception)
        {
            // Record failure metrics
            sw.Stop();
            _metrics.RecordHistogram(
                "request_duration_seconds",
                sw.Elapsed.TotalSeconds,
                tags);

            _metrics.IncrementCounter(
                "requests_failed_total",
                1,
                tags);

            throw;
        }
        finally
        {
            // Record current requests gauge
            _metrics.RecordGauge(
                "requests_in_progress",
                1,
                tags);
        }
    }
} 