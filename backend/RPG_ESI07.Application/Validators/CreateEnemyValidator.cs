using FluentValidation;
using RPG_ESI07.Application.Commands;

namespace RPG_ESI07.Application.Validators;

public class CreateEnemyValidator : AbstractValidator<CreateEnemyCommand>
{
    public CreateEnemyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(50).WithMessage("Name must not exceed 50 characters");

        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Type is required")
            .MaximumLength(20)
            .Must(x => new[] { "basic", "miniboss", "boss" }.Contains(x.ToLower()))
            .WithMessage("Type must be: basic, miniboss, or boss");

        RuleFor(x => x.MaxHP)
            .GreaterThan(0).WithMessage("MaxHP must be greater than 0");

        RuleFor(x => x.Strength)
            .GreaterThan(0).WithMessage("Strength must be greater than 0");

        RuleFor(x => x.Intelligence)
            .GreaterThanOrEqualTo(0).WithMessage("Intelligence must be >= 0");

        RuleFor(x => x.Speed)
            .GreaterThanOrEqualTo(0).WithMessage("Speed must be >= 0");

        RuleFor(x => x.PhysicalResistance)
            .InclusiveBetween(0.5f, 2.0f)
            .WithMessage("PhysicalResistance must be between 0.5 and 2.0");

        RuleFor(x => x.MagicalResistance)
            .InclusiveBetween(0.5f, 2.0f)
            .WithMessage("MagicalResistance must be between 0.5 and 2.0");

        RuleFor(x => x.ExperienceReward)
            .GreaterThanOrEqualTo(0).WithMessage("ExperienceReward must be >= 0");

        RuleFor(x => x.GoldReward)
            .GreaterThanOrEqualTo(0).WithMessage("GoldReward must be >= 0");
    }
}