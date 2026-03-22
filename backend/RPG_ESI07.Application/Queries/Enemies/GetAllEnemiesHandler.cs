using MediatR;
using RPG_ESI07.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Queries.Enemies; 

public class GetAllEnemiesHandler: IRequestHandler<GetAllEnemiesQuery, GetAllEnemiesResponse>
{
    private readonly IEnemyRepository _repository;

    public GetAllEnemiesHandler(IEnemyRepository repository)
    {
        _repository = repository; 
    }

    public async Task<GetAllEnemiesResponse> Handle(GetAllEnemiesQuery request, CancellationToken cancellationToken)
    {
        var enemies = await _repository.GetAllAsync();
        return new GetAllEnemiesResponse(enemies); 
    }
}
