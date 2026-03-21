using AutoMapper;
using MediatR;
using RPG_ESI07.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Commands; 

public class UpdateCharacterHandler:IRequestHandler<UpdateCharactersCommand, UpdateCharacterResponse>
{
    private readonly IPlayerProfileRepository _repository;
    private readonly IMapper _mapper;

    public UpdateCharacterHandler(IPlayerProfileRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper; 
    }

    public async Task<UpdateCharacterResponse> Handle(UpdateCharactersCommand request, CancellationToken cancellationToken)
    {
        var player = await _repository.GetByIdAsync(request.UserId);

        if (player == null)
            throw new InvalidOperationException($"User with id {request.UserId} cannot be found");

        _mapper.Map(request, player);
        await _repository.UpdateAsync(player);

        return new UpdateCharacterResponse("Character updated successfully"); 
    }
}
