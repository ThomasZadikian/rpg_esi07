using Moq;
using Xunit;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using AutoMapper;

namespace RPG_ESI07.Tests.Handlers.Commands;

public class UpdateCharacterHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_UpdatesCharacter()
    {
        // Arrange
        var existingCharacter = new PlayerProfile
        {
            Id = 1,
            UserId = 1,
            CharacterName = "OldName",
            Level = 1,
            CurrentHP = 100,
            MaxHP = 100,
            CurrentMP = 50,
            MaxMP = 50,
            Strength = 10,
            Intelligence = 10,
            Speed = 10,
            Experience = 0,
            Gold = 0
        };

        var mockRepo = new Mock<IPlayerProfileRepository>();
        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(m => m.Map(It.IsAny<UpdateCharactersCommand>(), It.IsAny<PlayerProfile>()))
            .Callback<UpdateCharactersCommand, PlayerProfile>((cmd, character) =>
            {
                character.CharacterName = cmd.CharacterName;
                character.Level = cmd.Level;
                character.CurrentHP = cmd.CurrentHP;
                character.MaxHP = cmd.MaxHP;
                character.CurrentMP = cmd.CurrentMP;
                character.MaxMP = cmd.MaxMP;
                character.Strength = cmd.Strength;
                character.Intelligence = cmd.Intelligence;
                character.Speed = cmd.Speed;
                character.Experience = cmd.Experience;
                character.Gold = cmd.Gold;
            });

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingCharacter);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<PlayerProfile>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateCharacterHandler(mockRepo.Object, mockMapper.Object);
        var command = new UpdateCharactersCommand(
            UserId: 1,
            CharacterName: "NewName",
            Level: 5,
            CurrentHP: 120,
            MaxHP: 120,
            CurrentMP: 60,
            MaxMP: 60,
            Strength: 15,
            Intelligence: 10,
            Speed: 10,
            Experience: 1000,
            Gold: 500
        );

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Character updated successfully", response.Message);
        mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<PlayerProfile>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ThrowsException()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((PlayerProfile?)null);

        var handler = new UpdateCharacterHandler(mockRepo.Object, mockMapper.Object);
        var command = new UpdateCharactersCommand(
            UserId: 999,
            CharacterName: "Ghost",
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

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None)
        );
    }
}