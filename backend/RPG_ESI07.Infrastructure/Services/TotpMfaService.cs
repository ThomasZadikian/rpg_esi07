using OtpNet;
using RPG_ESI07.Domain.Interfaces;
namespace RPG_ESI07.Infrastructure.Services;

public class TotpMfaService : IMfaService
{
    public byte[] GenerateSecret()
    {
        return KeyGeneration.GenerateRandomKey(20);
    }
    public string GetQrCodeUri(
    string secret, string username)
    {
        var base32Secret =
        Base32Encoding.ToString(
        System.Text.Encoding.UTF8
        .GetBytes(secret));
        return $"otpauth://totp/RPG_ESI07:{username}"
        + $"?secret={base32Secret}"
        + "&issuer;=RPG_ESI07";
    }
    public bool ValidateCode(
    byte[] secret, string code)
    {
        var totp = new Totp(secret);
        return totp.VerifyTotp(code,
        out _, new VerificationWindow(1, 1));
    }
}
