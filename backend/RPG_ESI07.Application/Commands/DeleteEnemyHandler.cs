using MediatR;
using Microsoft.AspNetCore.Http.Timeouts;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands;

public class DeleteEnemyHandler : IRequestHandler<DeleteEnemyCommand, DeleteEnemyResponse>
{
    private readonly IEnemyRepository _repository;

    public DeleteEnemyHandler(IEnemyRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteEnemyResponse> Handle(DeleteEnemyCommand request, CancellationToken cancellationToken)
    {
        var enemy = await _repository.GetByIdAsync(request.Id);

        if (enemy == null)
            throw new InvalidOperationException($"Enemy with id : {request.Id} does not exist.");

        await _repository.DeleteAsync(enemy.Id);

        return new DeleteEnemyResponse("Enemy deleted successfully");
    }
}