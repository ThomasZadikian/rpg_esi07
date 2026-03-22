using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries.Characters;

public record GetCharacterByIdQuery(int Id): IRequest<GetCharacterByIdResponse>;

public record GetCharacterByIdResponse(PlayerProfile playerProfile); 
