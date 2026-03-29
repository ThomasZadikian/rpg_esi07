using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.CombatStatss;

public class DeleteCombatStatsHandler : IRequestHandler<DeleteCombatStatsCommand, DeleteCombatStatsResponse>
{
    private readonly ICombatStatsRepository _repository;

    public DeleteCombatStatsHandler(ICombatStatsRepository repository) => _repository = repository;

    public async Task<DeleteCombatStatsResponse> Handle(DeleteCombatStatsCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return new DeleteCombatStatsResponse(true, "CombatStats deleted successfully");
    }
}
