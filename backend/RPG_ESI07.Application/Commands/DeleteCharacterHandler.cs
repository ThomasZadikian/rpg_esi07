using MediatR;
using RPG_ESI07.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Commands; 

public class DeleteCharacterHandler: IRequestHandler<DeleteCharacterCommand, DeleteCharacterResponse>
{
    private readonly IPlayerProfileRepository _repository; 

    public DeleteCharacterHandler(IPlayerProfileRepository repository)
    {
        _repository = repository;
    }

    public async Task<DeleteCharacterResponse> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.Id);

        if (player == null)
            throw new InvalidOperationException($"player with id : {request.Id} cannot be found");

        await _repository.DeleteAsync(request.Id);

        return new DeleteCharacterResponse("Player deleted successfully"); 
    }
}
