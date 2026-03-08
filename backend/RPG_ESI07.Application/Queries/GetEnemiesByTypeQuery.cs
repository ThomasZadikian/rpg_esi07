using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries;

public record GetEnemiesByTypeQuery(string Type) : IRequest<GetEnemiesByTypeResponse>;

public record GetEnemiesByTypeResponse(List<Enemy> Enemies); 
