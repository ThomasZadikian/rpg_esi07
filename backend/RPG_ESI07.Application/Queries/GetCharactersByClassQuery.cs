using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries;

public record GetCharactersByClassQuery(string Class) : IRequest<GetCharactersByClassResponse>;

public record GetCharactersByClassResponse(List<PlayerProfile> playerProfile); 
