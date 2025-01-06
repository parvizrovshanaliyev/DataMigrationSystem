using FluentValidation;

namespace DataMigration.Auth.Commands.VerifyMfa;

public class VerifyMfaCommandValidator : AbstractValidator<VerifyMfaCommand>
{
    public VerifyMfaCommandValidator()
    {
        RuleFor(x => x.MfaToken)
            .NotEmpty()
            .WithMessage("MFA token is required");

        RuleFor(x => x.Code)
            .NotEmpty()
            .Length(6)
            .Matches("^[0-9]+$")
            .WithMessage("MFA code must be 6 digits");
    }
} 