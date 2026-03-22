using FluentValidation;
using RPG_ESI07.Application.Commands.Characters;

public class UpdateCharacterValidator :
    AbstractValidator<UpdateCharactersCommand>
{
    public UpdateCharacterValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("Id must be > 0");

        RuleFor(x => x.CharacterName)
            .NotEmpty().WithMessage("CharacterName required")
            .MaximumLength(50).WithMessage("Max 50 characters");

        RuleFor(x => x.Level)
            .GreaterThan(0).WithMessage("Level > 0")
            .LessThanOrEqualTo(100).WithMessage("Level max 100");

        RuleFor(x => x.CurrentHP)
            .GreaterThan(0).WithMessage("CurrentHP > 0")
            .LessThanOrEqualTo(x => x.MaxHP)
            .WithMessage("CurrentHP <= MaxHP");

        RuleFor(x => x.MaxHP)
            .GreaterThan(0).WithMessage("MaxHP > 0");

        RuleFor(x => x.CurrentMP)
            .GreaterThanOrEqualTo(0).WithMessage("CurrentMP >= 0")
            .LessThanOrEqualTo(x => x.MaxMP)
            .WithMessage("CurrentMP <= MaxMP");

        RuleFor(x => x.MaxMP)
            .GreaterThanOrEqualTo(0).WithMessage("MaxMP >= 0");

        RuleFor(x => x.Strength)
            .GreaterThan(0).WithMessage("Strength > 0")
            .LessThanOrEqualTo(100).WithMessage("Strength max 100");

        RuleFor(x => x.Intelligence)
            .GreaterThan(0).WithMessage("Intelligence > 0")
            .LessThanOrEqualTo(100).WithMessage("Intelligence max 100");

        RuleFor(x => x.Speed)
            .GreaterThan(0).WithMessage("Speed > 0")
            .LessThanOrEqualTo(100).WithMessage("Speed max 100");

        RuleFor(x => x.Experience)
            .GreaterThanOrEqualTo(0).WithMessage("Experience >= 0");

        RuleFor(x => x.Gold)
            .GreaterThanOrEqualTo(0).WithMessage("Gold >= 0");
    }
}