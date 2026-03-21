using MediatR;

namespace RPG_ESI07.Application.Commands;

public record DeleteCharacterCommand(int Id) : IRequest<DeleteCharacterResponse>;

public record DeleteCharacterResponse(string Message); 