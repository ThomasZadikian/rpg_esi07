using MediatR;

namespace RPG_ESI07.Application.Commands;

public record CreateCharacterCommand
    (
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
    ) : IRequest<CreateCharacterResponse>;

public record CreateCharacterResponse(int Id, string Message); 
