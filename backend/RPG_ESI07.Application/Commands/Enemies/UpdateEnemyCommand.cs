using MediatR;

namespace RPG_ESI07.Application.Commands.Enemies;

public record UpdateEnemyCommand(
    int Id, 
    string Name,
    string Type,
    int MaxHP,
    int Strength,
    int Intelligence,
    int Speed,
    float PhysicalResistance,
    float MagicalResistance,
    int ExperienceReward,
    int GoldReward,
    string? Description) : IRequest<UpdateEnemyResponse>;

public record UpdateEnemyResponse(string Message); 
