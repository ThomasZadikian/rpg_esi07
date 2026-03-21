using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries;

public record GetCharactersByLevelQuery(int Level) : IRequest<GetCharactersByLevelResponse>;

public record GetCharactersByLevelResponse(List<PlayerProfile> playerProfiles); 