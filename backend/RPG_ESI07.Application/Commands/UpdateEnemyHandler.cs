using AutoMapper;
using MediatR;
using RPG_ESI07.Application.Mappers;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Application.Commands; 

public class UpdateEnemyHandler: IRequestHandler<UpdateEnemyCommand, UpdateEnemyResponse>
{
    private readonly IEnemyRepository _repository;
    private readonly IMapper _mapper; 

    public UpdateEnemyHandler(IEnemyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper; 
    }

    public async Task<UpdateEnemyResponse> Handle(UpdateEnemyCommand request, CancellationToken cancellationToken)
    {
        var enemy = await _repository.GetByIdAsync(request.Id); 
        
        if (enemy == null)
            throw new InvalidOperationException($"Enemy with id : {request.Id} cannot be found");

        _mapper.Map(request, enemy);
        await _repository.UpdateAsync(enemy);

        return new UpdateEnemyResponse("Enemy update successfly"); 

    }
}