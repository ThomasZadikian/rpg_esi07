using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries.Users;

public record GetAllUsersQuery : IRequest<GetAllUsersResponse>;

public record GetAllUsersResponse(List<User> Items);
