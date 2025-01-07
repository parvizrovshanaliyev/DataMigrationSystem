using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using DataMigration.Application.Common.Interfaces;

namespace DataMigration.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for handling database transactions
/// </summary>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.Id;

        try
        {
            if (request is ITransactionalRequest)
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);

                _logger.LogInformation(
                    "Beginning transaction for {RequestName} ({UserId})", 
                    requestName, 
                    userId);

                var response = await next();

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                _logger.LogInformation(
                    "Committed transaction for {RequestName} ({UserId})", 
                    requestName, 
                    userId);

                return response;
            }

            // No transaction required
            return await next();
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex, 
                "Error processing transaction for {RequestName} ({UserId})", 
                requestName, 
                userId);

            await _unitOfWork.RollbackTransactionAsync(cancellationToken);

            _logger.LogInformation(
                "Rolled back transaction for {RequestName} ({UserId})", 
                requestName, 
                userId);

            throw;
        }
    }
} 