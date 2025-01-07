using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using DataMigration.Application.Common.Interfaces;
using DataMigration.Application.Common.Attributes;

namespace DataMigration.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for caching request responses
/// </summary>
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
    private readonly ICacheService _cache;
    private readonly ICurrentUser _currentUser;

    public CachingBehavior(
        ILogger<CachingBehavior<TRequest, TResponse>> logger,
        ICacheService cache,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _cache = cache;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var attribute = request.GetType().GetCustomAttributes(typeof(CacheAttribute), false);
        if (attribute.Length == 0)
        {
            return await next();
        }

        var cacheAttribute = (CacheAttribute)attribute[0];
        var cacheKey = BuildCacheKey(request, cacheAttribute);

        try
        {
            // Try to get from cache
            var cachedResponse = await _cache.GetAsync<TResponse>(cacheKey);
            if (cachedResponse != null)
            {
                _logger.LogInformation(
                    "Cache hit for {RequestName} with key {CacheKey}",
                    typeof(TRequest).Name,
                    cacheKey);

                return cachedResponse;
            }

            // Cache miss, execute request
            var response = await next();

            // Cache the response
            await _cache.SetAsync(
                cacheKey,
                response,
                TimeSpan.FromSeconds(cacheAttribute.DurationInSeconds));

            _logger.LogInformation(
                "Cache miss for {RequestName}, cached with key {CacheKey}",
                typeof(TRequest).Name,
                cacheKey);

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error processing cache for {RequestName} with key {CacheKey}",
                typeof(TRequest).Name,
                cacheKey);

            return await next();
        }
    }

    private string BuildCacheKey(TRequest request, CacheAttribute attribute)
    {
        var prefix = attribute.Prefix ?? typeof(TRequest).Name;
        var userId = _currentUser.Id?.ToString() ?? "anonymous";
        var requestHash = JsonSerializer.Serialize(request).GetHashCode().ToString();

        return $"{prefix}:{userId}:{requestHash}";
    }
} 