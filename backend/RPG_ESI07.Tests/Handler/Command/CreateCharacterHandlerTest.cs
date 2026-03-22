using Moq;
using Xunit;
using RPG_ESI07.Application.Commands.Characters;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Commands;

public class CreateCharacterHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_CreatesCharacter()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<PlayerProfile>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateCharacterHandler(mockRepo.Object);
        var command = new CreateCharacterCommand(
            UserId: 1,
            CharacterName: "Hero",
            Level: 1,
            CurrentHP: 100,
            MaxHP: 100,
            CurrentMP: 50,
            MaxMP: 50,
            Strength: 10,
            Intelligence: 10,
            Speed: 10,
            Experience: 0,
            Gold: 0
        );

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Character created successfully", response.Message);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<PlayerProfile>()), Times.Once);
    }
}