using MediatR;

namespace RPG_ESI07.Application.Commands.PlayerSkills;

public record DeletePlayerSkillCommand(int Id) : IRequest<DeletePlayerSkillResponse>;

public record DeletePlayerSkillResponse(bool Success, string Message);
