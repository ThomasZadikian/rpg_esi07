using MediatR;

namespace RPG_ESI07.Application.Commands;

public record DeleteEnemyCommand(int Id) : IRequest<DeleteEnemyResponse>;

public record DeleteEnemyResponse(string Message); 
