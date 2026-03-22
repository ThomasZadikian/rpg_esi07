using FluentValidation;
using RPG_ESI07.Application.Commands.Characters;

namespace RPG_ESI07.Application.Validators;

public class CreateCharacterValidator :
    AbstractValidator<CreateCharacterCommand>
{
    public CreateCharacterValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than 0");

        RuleFor(x => x.CharacterName)
            .NotEmpty().WithMessage("CharacterName is required")
            .MaximumLength(50).WithMessage("CharacterName max 50 characters");

        RuleFor(x => x.Level)
            .GreaterThan(0).WithMessage("Level must be > 0")
            .LessThanOrEqualTo(100).WithMessage("Level max 100");

        RuleFor(x => x.CurrentHP)
            .GreaterThan(0).WithMessage("CurrentHP must be > 0")
            .LessThanOrEqualTo(x => x.MaxHP)
            .WithMessage("CurrentHP cannot exceed MaxHP");

        RuleFor(x => x.MaxHP)
            .GreaterThan(0).WithMessage("MaxHP must be > 0");

        RuleFor(x => x.CurrentMP)
            .GreaterThanOrEqualTo(0).WithMessage("CurrentMP >= 0")
            .LessThanOrEqualTo(x => x.MaxMP)
            .WithMessage("CurrentMP cannot exceed MaxMP");

        RuleFor(x => x.MaxMP)
            .GreaterThanOrEqualTo(0).WithMessage("MaxMP >= 0");

        RuleFor(x => x.Strength)
            .GreaterThan(0).WithMessage("Strength must be > 0")
            .LessThanOrEqualTo(100).WithMessage("Strength max 100");

        RuleFor(x => x.Intelligence)
            .GreaterThan(0).WithMessage("Intelligence must be > 0")
            .LessThanOrEqualTo(100).WithMessage("Intelligence max 100");

        RuleFor(x => x.Speed)
            .GreaterThan(0).WithMessage("Speed must be > 0")
            .LessThanOrEqualTo(100).WithMessage("Speed max 100");

        RuleFor(x => x.Experience)
            .GreaterThanOrEqualTo(0).WithMessage("Experience >= 0");

        RuleFor(x => x.Gold)
            .GreaterThanOrEqualTo(0).WithMessage("Gold >= 0");
    }
}