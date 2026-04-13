using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RPG_ESI07.API.Controllers;
using RPG_ESI07.Application.Commands.Auth;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RPG_ESI07.Tests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mediatorMock = new Mock<IMediator>(MockBehavior.Strict);
        _controller = new AuthController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsOkResult_WhenRegistrationSucceeds()
    {
        // Arrange
        var command = new RegisterCommand("NouveauJoueur", "joueur@rpg.com", "MotDePasse123!");

        // success = true
        var expectedResponse = new AuthResponse(true, "fake-jwt-token", false, "Inscription réussie");

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Register(command);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenRegistrationFails()
    {
        // Arrange
        var command = new RegisterCommand("JoueurExistant", "existant@rpg.com", "MotDePasse123!");

        // success = false
        var failedResponse = new AuthResponse(false, null, false, "Cet email est déjà utilisé");

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(failedResponse);

        // Act
        var result = await _controller.Register(command);

        // Assert
        var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().BeEquivalentTo(failedResponse);
    }

    [Fact]
    public async Task Login_ReturnsOkResult_WhenLoginSucceeds()
    {
        // Arrange
        var command = new LoginCommand("MonUser", "MonPassword!");

        // success = true
        var expectedResponse = new AuthResponse(true, "valid-jwt-token", false, "Connexion réussie");

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(expectedResponse);

        // Act
        var result = await _controller.Login(command);

        // Assert
        var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be(200);
        okResult.Value.Should().BeEquivalentTo(expectedResponse);
    }

    [Fact]
    public async Task Login_ReturnsUnauthorized_WhenLoginFails()
    {
        // Arrange
        var command = new LoginCommand("MonUser", "MauvaisPassword");

        // success = false
        var failedResponse = new AuthResponse(false, null, false, "Identifiants invalides");

        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                     .ReturnsAsync(failedResponse);

        // Act
        var result = await _controller.Login(command);

        // Assert
        var unauthorizedResult = result.Should().BeOfType<UnauthorizedObjectResult>().Subject;
        unauthorizedResult.StatusCode.Should().Be(401);
        unauthorizedResult.Value.Should().BeEquivalentTo(failedResponse);
    }
}