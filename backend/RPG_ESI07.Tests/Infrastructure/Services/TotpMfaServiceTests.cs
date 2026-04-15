using FluentAssertions;
using OtpNet;
using RPG_ESI07.Infrastructure.Services;
using System.Text;

namespace RPG_ESI07.Tests.Infrastructure.Services;

public class TotpMfaServiceTests
{
    private readonly TotpMfaService _service;

    public TotpMfaServiceTests()
    {
        _service = new TotpMfaService();
    }

    [Fact]
    public void GenerateSecret_Returns20ByteArray()
    {
        var secret = _service.GenerateSecret();

        secret.Should().NotBeNull();
        secret.Should().HaveCount(20);
    }

    [Fact]
    public void GetQrCodeUri_FormatsUriCorrectly()
    {
        var secret = "mysecretkey";
        var username = "testuser";
        var base32Expected = Base32Encoding.ToString(Encoding.UTF8.GetBytes(secret));

        var uri = _service.GetQrCodeUri(secret, username);

        uri.Should().StartWith("otpauth://totp/RPG_ESI07:testuser");
        uri.Should().Contain($"?secret={base32Expected}");
        uri.Should().Contain("&issuer=RPG_ESI07");
    }

    [Fact]
    public void ValidateCode_ReturnsTrue_WhenCodeIsValid()
    {
        var secret = _service.GenerateSecret();
        var totp = new Totp(secret);
        var validCode = totp.ComputeTotp();

        var result = _service.ValidateCode(secret, validCode);

        result.Should().BeTrue();
    }

    [Fact]
    public void ValidateCode_ReturnsFalse_WhenCodeIsInvalid()
    {
        var secret = _service.GenerateSecret();
        var invalidCode = "000000"; // Fortes probabilités d'être invalide

        var result = _service.ValidateCode(secret, invalidCode);

        result.Should().BeFalse();
    }
}