using Moq;
using Xunit;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Commands;

public class DeleteEnemyHandlerTests
{
    [Fact]
    public async Task Handle_WithValidId_DeletesEnemy()
    {
        // Arrange
        var enemy = new Enemy
        {
            Id = 1,
            Name = "Goblin",
            Type = "basic"
        };

        var mockRepo = new Mock<IEnemyRepository>();

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(enemy);

        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteEnemyHandler(mockRepo.Object);
        var command = new DeleteEnemyCommand(1);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Enemy deleted successfully", response.Message);
        mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ThrowsException()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Enemy?)null);

        var handler = new DeleteEnemyHandler(mockRepo.Object);
        var command = new DeleteEnemyCommand(1);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None)
        );
    }
}