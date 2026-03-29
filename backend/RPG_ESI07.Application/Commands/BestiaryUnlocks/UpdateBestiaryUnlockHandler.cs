using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.BestiaryUnlocks;

public class UpdateBestiaryUnlockHandler : IRequestHandler<UpdateBestiaryUnlockCommand, UpdateBestiaryUnlockResponse>
{
    private readonly IBestiaryUnlockRepository _repository;

    public UpdateBestiaryUnlockHandler(IBestiaryUnlockRepository repository) => _repository = repository;

    public async Task<UpdateBestiaryUnlockResponse> Handle(UpdateBestiaryUnlockCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return new UpdateBestiaryUnlockResponse(false, "BestiaryUnlock not found");

        entity.PlayerId = request.PlayerId;
        entity.EnemyId = request.EnemyId;
        await _repository.UpdateAsync(entity);
        return new UpdateBestiaryUnlockResponse(true, "BestiaryUnlock updated successfully");
    }
}
