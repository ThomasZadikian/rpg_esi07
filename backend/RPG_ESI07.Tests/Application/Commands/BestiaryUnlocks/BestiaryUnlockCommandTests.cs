using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Commands.BestiaryUnlocks;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Application.Commands.BestiaryUnlocks;

public class CreateBestiaryUnlockHandlerTests
{
    private readonly Mock<IBestiaryUnlockRepository> _mockRepo;
    private readonly CreateBestiaryUnlockHandler _handler;

    public CreateBestiaryUnlockHandlerTests()
    {
        _mockRepo = new Mock<IBestiaryUnlockRepository>();
        _handler = new CreateBestiaryUnlockHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CreateBestiaryUnlockCommand(1, 2);
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<BestiaryUnlock>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be("BestiaryUnlock created successfully");
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<BestiaryUnlock>()), Times.Once);
    }
}

public class UpdateBestiaryUnlockHandlerTests
{
    private readonly Mock<IBestiaryUnlockRepository> _mockRepo;
    private readonly UpdateBestiaryUnlockHandler _handler;

    public UpdateBestiaryUnlockHandlerTests()
    {
        _mockRepo = new Mock<IBestiaryUnlockRepository>();
        _handler = new UpdateBestiaryUnlockHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ExistingEntity_ReturnsSuccess()
    {
        // Arrange
        var entity = new BestiaryUnlock { Id = 1, PlayerId = 1, EnemyId = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<BestiaryUnlock>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new UpdateBestiaryUnlockCommand(1, 2, 3), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        entity.PlayerId.Should().Be(2);
        entity.EnemyId.Should().Be(3);
    }

    [Fact]
    public async Task Handle_NonExistingEntity_ReturnsFailure()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((BestiaryUnlock?)null);

        // Act
        var result = await _handler.Handle(new UpdateBestiaryUnlockCommand(99, 1, 1), CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }
}

public class DeleteBestiaryUnlockHandlerTests
{
    private readonly Mock<IBestiaryUnlockRepository> _mockRepo;
    private readonly DeleteBestiaryUnlockHandler _handler;

    public DeleteBestiaryUnlockHandlerTests()
    {
        _mockRepo = new Mock<IBestiaryUnlockRepository>();
        _handler = new DeleteBestiaryUnlockHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsSuccess()
    {
        // Arrange
        _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new DeleteBestiaryUnlockCommand(1), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        _mockRepo.Verify(r => r.DeleteAsync(1), Times.Once);
    }
}
