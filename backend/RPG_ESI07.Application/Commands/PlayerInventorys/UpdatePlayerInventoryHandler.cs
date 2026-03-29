using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.PlayerInventorys;

public class UpdatePlayerInventoryHandler : IRequestHandler<UpdatePlayerInventoryCommand, UpdatePlayerInventoryResponse>
{
    private readonly IPlayerInventoryRepository _repository;

    public UpdatePlayerInventoryHandler(IPlayerInventoryRepository repository) => _repository = repository;

    public async Task<UpdatePlayerInventoryResponse> Handle(UpdatePlayerInventoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(request.Id);
        if (entity == null) return new UpdatePlayerInventoryResponse(false, "PlayerInventory not found");

        entity.PlayerId = request.PlayerId;
        entity.ItemId = request.ItemId;
        entity.Quantity = request.Quantity;
        await _repository.UpdateAsync(entity);
        return new UpdatePlayerInventoryResponse(true, "PlayerInventory updated successfully");
    }
}
