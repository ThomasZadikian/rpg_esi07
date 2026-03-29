using MediatR;

namespace RPG_ESI07.Application.Commands.CombatStatss;

public record DeleteCombatStatsCommand(int Id) : IRequest<DeleteCombatStatsResponse>;

public record DeleteCombatStatsResponse(bool Success, string Message);
