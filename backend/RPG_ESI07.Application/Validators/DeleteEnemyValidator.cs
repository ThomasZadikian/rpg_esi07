using FluentValidation;
using RPG_ESI07.Application.Commands.Enemies;

namespace RPG_ESI07.Application.Validators;
public class DeleteEnemyValidator : AbstractValidator<DeleteEnemyCommand>
{
    public DeleteEnemyValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id cannot be egal to 0"); 
    }
}
