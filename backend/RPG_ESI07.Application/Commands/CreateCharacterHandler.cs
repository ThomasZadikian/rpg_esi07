using MediatR;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using System.Diagnostics;

namespace RPG_ESI07.Application.Commands; 

public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, CreateCharacterResponse>
{
    private readonly IPlayerProfileRepository _repository;

    public CreateCharacterHandler(IPlayerProfileRepository repository)
    {
        _repository = repository; 
    }

    public async Task<CreateCharacterResponse> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
    {
        var character = new PlayerProfile
        {
            UserId = request.UserId,
            CharacterName = request.CharacterName,
            Level = request.Level,
            CurrentHP = request.CurrentHP,
            MaxHP = request.MaxHP,
            CurrentMP = request.CurrentMP,
            MaxMP = request.MaxMP,
            Strength = request.Strength,
            Intelligence = request.Intelligence,
            Speed = request.Speed,
            Experience = request.Experience,
            Gold = request.Gold,
        };

        await _repository.AddAsync(character);

        return new CreateCharacterResponse(character.Id, "Character created successfully"); 
    }
}
