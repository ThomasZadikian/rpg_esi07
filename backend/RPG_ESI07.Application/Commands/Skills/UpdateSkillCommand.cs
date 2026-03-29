using MediatR;

namespace RPG_ESI07.Application.Commands.Skills;

public record UpdateSkillCommand(
    int Id, string Name, string EffectType, int MPCost
) : IRequest<UpdateSkillResponse>;

public record UpdateSkillResponse(bool Success, string Message);
