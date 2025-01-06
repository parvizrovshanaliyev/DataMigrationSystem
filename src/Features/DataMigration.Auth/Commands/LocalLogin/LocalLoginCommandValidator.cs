using FluentValidation;

namespace DataMigration.Auth.Commands.LocalLogin;

public class LocalLoginCommandValidator : AbstractValidator<LocalLoginCommand>
{
    public LocalLoginCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email address is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long");
    }
} 