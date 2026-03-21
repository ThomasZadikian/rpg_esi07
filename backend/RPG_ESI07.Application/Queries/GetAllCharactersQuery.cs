using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries;

public record GetAllCharactersQuery : IRequest<GetAllCharactersResponse>;

public record GetAllCharactersResponse(List<PlayerProfile> playerProfile); 