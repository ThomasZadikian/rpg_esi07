using MediatR;

namespace RPG_ESI07.Application.Commands.PlayerSkills;

public record UpdatePlayerSkillCommand(
    int Id, int PlayerId, int SkillId
) : IRequest<UpdatePlayerSkillResponse>;

public record UpdatePlayerSkillResponse(bool Success, string Message);
