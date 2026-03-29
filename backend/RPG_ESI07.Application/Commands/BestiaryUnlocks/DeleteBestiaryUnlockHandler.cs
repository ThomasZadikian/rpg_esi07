using MediatR;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands.BestiaryUnlocks;

public class DeleteBestiaryUnlockHandler : IRequestHandler<DeleteBestiaryUnlockCommand, DeleteBestiaryUnlockResponse>
{
    private readonly IBestiaryUnlockRepository _repository;

    public DeleteBestiaryUnlockHandler(IBestiaryUnlockRepository repository) => _repository = repository;

    public async Task<DeleteBestiaryUnlockResponse> Handle(DeleteBestiaryUnlockCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return new DeleteBestiaryUnlockResponse(true, "BestiaryUnlock deleted successfully");
    }
}
