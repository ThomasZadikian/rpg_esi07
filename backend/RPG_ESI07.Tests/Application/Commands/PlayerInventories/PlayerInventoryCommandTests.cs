using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Commands.PlayerInventorys;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Application.Commands.PlayerInventories;

public class CreatePlayerInventoryHandlerTests
{
    private readonly Mock<IPlayerInventoryRepository> _mockRepo;
    private readonly CreatePlayerInventoryHandler _handler;

    public CreatePlayerInventoryHandlerTests()
    {
        _mockRepo = new Mock<IPlayerInventoryRepository>();
        _handler = new CreatePlayerInventoryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsSuccess()
    {
        // Arrange
        _mockRepo.Setup(r => r.AddAsync(It.IsAny<PlayerInventory>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new CreatePlayerInventoryCommand(1, 2, 5), CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Message.Should().Be("PlayerInventory created successfully");
    }
}

public class UpdatePlayerInventoryHandlerTests
{
    private readonly Mock<IPlayerInventoryRepository> _mockRepo;
    private readonly UpdatePlayerInventoryHandler _handler;

    public UpdatePlayerInventoryHandlerTests()
    {
        _mockRepo = new Mock<IPlayerInventoryRepository>();
        _handler = new UpdatePlayerInventoryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ExistingEntity_UpdatesQuantity()
    {
        // Arrange
        var entity = new PlayerInventory { Id = 1, PlayerId = 1, ItemId = 1, Quantity = 1 };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(entity);
        _mockRepo.Setup(r => r.UpdateAsync(It.IsAny<PlayerInventory>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new UpdatePlayerInventoryCommand(1, 1, 2, 10), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        entity.Quantity.Should().Be(10);
        entity.ItemId.Should().Be(2);
    }

    [Fact]
    public async Task Handle_NonExistingEntity_ReturnsFailure()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((PlayerInventory?)null);

        // Act
        var result = await _handler.Handle(new UpdatePlayerInventoryCommand(99, 1, 1, 1), CancellationToken.None);

        // Assert
        result.Success.Should().BeFalse();
    }
}

public class DeletePlayerInventoryHandlerTests
{
    private readonly Mock<IPlayerInventoryRepository> _mockRepo;
    private readonly DeletePlayerInventoryHandler _handler;

    public DeletePlayerInventoryHandlerTests()
    {
        _mockRepo = new Mock<IPlayerInventoryRepository>();
        _handler = new DeletePlayerInventoryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ValidId_CallsDelete()
    {
        // Arrange
        _mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>())).Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(new DeletePlayerInventoryCommand(1), CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
    }
}
