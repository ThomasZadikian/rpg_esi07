using MediatR;

namespace RPG_ESI07.Application.Commands.Characters;

public record UpdateCharactersCommand(
    int UserId,
    string CharacterName,
    int Level,
    int CurrentHP,
    int MaxHP,
    int CurrentMP,
    int MaxMP,
    int Strength,
    int Intelligence,
    int Speed,
    int Experience,
    int Gold
    ) : IRequest<UpdateCharacterResponse>;

public record UpdateCharacterResponse(string Message); 