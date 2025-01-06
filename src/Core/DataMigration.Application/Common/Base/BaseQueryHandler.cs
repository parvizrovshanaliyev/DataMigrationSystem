//using System.ComponentModel.DataAnnotations;
//using MediatR;
//using Microsoft.Extensions.Logging;
//using FluentValidation;

//namespace DataMigration.Application.Common.Base;



//public abstract class BaseCommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
//    where TCommand : IRequest<TResult>
//{
//    protected readonly ILogger<BaseCommandHandler<TCommand, TResult>> _logger;
//    protected readonly IValidator<TCommand>? _validator;

//    protected BaseCommandHandler(
//        ILogger<BaseCommandHandler<TCommand, TResult>> logger,
//        IValidator<TCommand>? validator = null)
//    {
//        _logger = logger;
//        _validator = validator;
//    }

//    public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
//    {
//        try
//        {
//            _logger.LogInformation("Handling command {CommandName}", typeof(TCommand).Name);

//            if (_validator != null)
//            {
//                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
//                if (!validationResult.IsValid)
//                {
//                    throw new ValidationException(validationResult.Errors);
//                }
//            }

//            var result = await HandleCommand(request, cancellationToken);

//            _logger.LogInformation("Command {CommandName} handled successfully", typeof(TCommand).Name);

//            return result;
//        }
//        catch (Exception ex) when (ex is not ValidationException)
//        {
//            _logger.LogError(ex, "Error handling command {CommandName}", typeof(TCommand).Name);
//            throw;
//        }
//    }

//    protected abstract Task<TResult> HandleCommand(TCommand command, CancellationToken cancellationToken);
//} 