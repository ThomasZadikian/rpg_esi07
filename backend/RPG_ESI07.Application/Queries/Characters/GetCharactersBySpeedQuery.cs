using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries.Characters;

public record GetCharactersBySpeedQuery() : IRequest<GetCharactersBySpeedResponse>;

public record GetCharactersBySpeedResponse(List<PlayerProfile> playerProfile); 
