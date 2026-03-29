using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Commands.UserConsents;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Application.Commands.UserConsents;

public class CreateUserConsentHandlerTests
{
    private readonly Mock<IUserConsentRepository> _mockRepo;
    private readonly CreateUserConsentHandler _handler;

    public CreateUserConsentHandlerTests()
    {
        _mockRepo = new Mock<IUserConsentRepository>();
        _handler = new CreateUserConsentHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<UserConsent>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new CreateUserConsentCommand(1, true, false), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be("UserConsent created successfully");
    }
}

public class UpdateUserConsentHandlerTests
{
    private readonly Mock<IUserConsentRepository> _mockRepo;
    private readonly UpdateUserConsentHandler _handler;

    public UpdateUserConsentHandlerTests()
    {
        _mockRepo = new Mock<IUserConsentRepository>();
        _handler = new UpdateUserConsentHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ExistingEntity_UpdatesConsent()
    {
        // Arrange
        var entity = new UserConsent { Id = 1, UserId = 1, AnalyticsConsent = false, MarketingConsent = false };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<UserConsent>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new UpdateUserConsentCommand(1, 1, true, true), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        entity.AnalyticsConsent.Should().BeTrue();
        entity.MarketingConsent.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_NonExistingEntity_ReturnsFailure()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((UserConsent?)null);

        // Act
        var result = await _handler.Handle(new UpdateUserConsentCommand(99, 1, true, true), CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }
}

public class DeleteUserConsentHandlerTests
{
    private readonly Mock<IUserConsentRepository> _mockRepo;
    private readonly DeleteUserConsentHandler _handler;

    public DeleteUserConsentHandlerTests()
    {
        _mockRepo = new Mock<IUserConsentRepository>();
        _handler = new DeleteUserConsentHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidId_CallsDelete()
    {
        // Arrange
        _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new DeleteUserConsentCommand(1), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }
}
