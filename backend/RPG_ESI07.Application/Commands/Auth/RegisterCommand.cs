using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Application.Commands.Auth;

public record RegisterCommand(
    string Username,
    string Email,
    string Password) : IRequest<AuthResponse>;

public record AuthResponse(
    bool success,
    string? token,
    bool RequresMfa,
    string Message); 