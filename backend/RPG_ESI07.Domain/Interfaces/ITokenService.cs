using RPG_ESI07.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Domain.Interfaces; 

public interface ITokenService
{
    string GenerateAccessToken(User user, string role);
    string GenerateMfaToken(User user);
    int? ValidateTokenAndGetUserId(string token); 
}