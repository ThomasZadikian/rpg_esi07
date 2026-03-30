using FluentValidation;
using RPG_ESI07.Application.Commands.Auth;
namespace RPG_ESI07.Application.Validators;

public class RegisterValidator
: AbstractValidator<RegisterCommand>
{
    public RegisterValidator()
    {
        RuleFor(x => x.Username)
        .NotEmpty()
        .MinimumLength(3)
        .MaximumLength(50)
        .Matches("^[a-zA-Z0-9_]+$")
        .WithMessage(
        "Alphanumeric and underscore only");
        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();
        RuleFor(x => x.Password)
        .NotEmpty()
        .MinimumLength(8)
        .Matches("[A-Z]")
        .WithMessage("One uppercase required")
        .Matches("[0-9]")
        .WithMessage("One digit required")
        .Matches("[^a-zA-Z0-9]")
        .WithMessage("One special character required");
    }
}
public class LoginValidator
: AbstractValidator<LoginCommand>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}