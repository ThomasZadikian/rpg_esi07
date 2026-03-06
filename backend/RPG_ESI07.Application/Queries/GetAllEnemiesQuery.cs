using MediatR;
using RPG_ESI07.Domain.Entities;
using System.Collections.Generic; 

namespace RPG_ESI07.Application.Queries;

public record GetAllEnemiesQuery : IRequest<GetAllEnemiesResponse>;

public record GetAllEnemiesResponse(List<Enemy> Enemies); 

