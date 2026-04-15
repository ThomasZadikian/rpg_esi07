using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.BestiaryUnlocks;

public class CreateBestiaryUnlockHandler : IRequestHandler<CreateBestiaryUnlockCommand, CreateBestiaryUnlockResponse>
{
    private readonly IBestiaryUnlockRepository _repository;

    public CreateBestiaryUnlockHandler(IBestiaryUnlockRepository repository)
    {
        _repository = repository;
    }

    public async Task<CreateBestiaryUnlockResponse> Handle(CreateBestiaryUnlockCommand request, CancellationToken cancellationToken)
    {
        var entity = new BestiaryUnlock();
        await _repository.AddAsync(entity);
        return new CreateBestiaryUnlockResponse(entity.Id, "BestiaryUnlock created successfully");
    }
}