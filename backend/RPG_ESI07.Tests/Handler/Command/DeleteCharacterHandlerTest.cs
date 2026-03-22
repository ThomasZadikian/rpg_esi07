using Moq;
using Xunit;
using RPG_ESI07.Application.Commands.Characters;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Commands;

public class DeleteCharacterHandlerTests
{
    [Fact]
    public async Task Handle_WithValidId_DeletesCharacter()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new PlayerProfile { Id = 1, CharacterName = "Hero" });
        mockRepo.Setup(r => r.DeleteAsync(It.IsAny<int>()))
            .Returns(Task.CompletedTask);

        var handler = new DeleteCharacterHandler(mockRepo.Object);
        var command = new DeleteCharacterCommand(1);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Character deleted successfully", response.Message);
        mockRepo.Verify(r => r.DeleteAsync(It.IsAny<int>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ThrowsException()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((PlayerProfile?)null);

        var handler = new DeleteCharacterHandler(mockRepo.Object);
        var command = new DeleteCharacterCommand(999);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None)
        );
    }
}