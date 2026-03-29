using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.Items;

public class CreateItemHandler : IRequestHandler<CreateItemCommand, CreateItemResponse>
{
    private readonly IItemRepository _repository;

    public CreateItemHandler(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateItemResponse> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new Item();
        await _repository.AddAsync(entity);
        return new CreateItemResponse(entity.Id, "Item created successfully");
    }
}
