using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Commands.RGPD;

public record AnonymizeUserCommand(int UserId, string? Reason) : IRequest<AnonymizeUserResponse>;

public record AnonymizeUserResponse(bool Success, string Message); 
