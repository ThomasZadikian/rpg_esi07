using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using System.Globalization;

namespace RPG_ESI07.Application.Commands.Enemies; 

public class CreateEnemyHandler: IRequestHandler<CreateEnemyCommand, CreateEnemyResponse>
{
    private readonly IEnemyRepository _repository; 

    public CreateEnemyHandler(IEnemyRepository repository)
    {
        _repository = repository; 
    }

    public async Task<CreateEnemyResponse> Handle(CreateEnemyCommand request, CancellationToken cancellationToken)
    {
        var enemy = new Enemy
        {
            Name = request.Name,
            Type = request.Type,
            MaxHP = request.MaxHP,
            Strength = request.Strength,
            Intelligence = request.Intelligence,
            Speed = request.Speed,
            PhysicalResistance = request.PhysicalResistance,
            MagicalResistance = request.MagicalResistance,
            ExperienceReward = request.ExperienceReward,
            GoldReward = request.GoldReward,
            Description = request.Description
        };
        await _repository.AddAsync(enemy);

        return new CreateEnemyResponse(enemy.Id, "Enemy created successfully"); 
    }
}