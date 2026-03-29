using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.PlayerSkills;

public class DeletePlayerSkillHandler : IRequestHandler<DeletePlayerSkillCommand, DeletePlayerSkillResponse>
{
    private readonly IPlayerSkillRepository _repository;

    public DeletePlayerSkillHandler(IPlayerSkillRepository repository) => _repository = repository;

    public async Task<DeletePlayerSkillResponse> Handle(DeletePlayerSkillCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return new DeletePlayerSkillResponse(true, "PlayerSkill deleted successfully");
    }
}
