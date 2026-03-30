using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Domain.Interfaces; 
public interface IMfaService
{
    byte[] GenerateSecret();
    string GetQrCodeUri(string secret, string username);
    bool ValidateCode(byte[] secret, string code); 
}
