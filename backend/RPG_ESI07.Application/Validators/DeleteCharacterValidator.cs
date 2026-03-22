using FluentValidation;
using RPG_ESI07.Application.Commands.Characters;

public class DeleteCharacterValidator :
    AbstractValidator<DeleteCharacterCommand>
{
    public DeleteCharacterValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}