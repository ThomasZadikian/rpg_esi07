using MediatR;

namespace RPG_ESI07.Application.Commands.Skills;

public record CreateSkillCommand(
    string Name, string EffectType, int MPCost
) : IRequest<CreateSkillResponse>;

public record CreateSkillResponse(int Id, string Message);
