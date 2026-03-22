using MediatR;

namespace RPG_ESI07.Application.Commands.Characters;

public record DeleteCharacterCommand(int Id) : IRequest<DeleteCharacterResponse>;

public record DeleteCharacterResponse(string Message); 