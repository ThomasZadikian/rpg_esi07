using MediatR;
using RPG_ESI07.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Queries.PlayerInventorys;

public record GetPlayerInventoryByIdQuery(int Id, int RequestingUserId, bool IsAdmin) : IRequest<GetPlayerInventoryByIdResponse>;

public record GetPlayerInventoryByIdResponse(PlayerInventory playerInventory); 
