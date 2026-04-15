using MediatR;

namespace RPG_ESI07.Application.Commands.Items;

public record CreateItemCommand(
    string Name, string Type, int Price
) : IRequest<CreateItemResponse>;

public record CreateItemResponse(int Id, string Message);