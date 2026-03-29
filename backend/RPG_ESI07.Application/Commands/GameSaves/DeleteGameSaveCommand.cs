using MediatR;

namespace RPG_ESI07.Application.Commands.GameSaves;

public record DeleteGameSaveCommand(int Id) : IRequest<DeleteGameSaveResponse>;

public record DeleteGameSaveResponse(bool Success, string Message);
