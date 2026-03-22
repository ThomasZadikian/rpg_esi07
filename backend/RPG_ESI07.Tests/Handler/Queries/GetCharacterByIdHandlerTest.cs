using Moq;
using Xunit;
using RPG_ESI07.Application.Queries.Characters;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Queries;

public class GetCharacterByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithValidId_ReturnsCharacter()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        var character = new PlayerProfile
        {
            Id = 1,
            UserId = 1,
            CharacterName = "Hero",
            Level = 5,
            Strength = 15
        };
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(character);

        var handler = new GetCharacterByIdHandler(mockRepo.Object);
        var query = new GetCharacterByIdQuery(1);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(response.playerProfile);
        Assert.Equal("Hero", response.playerProfile.CharacterName);
        Assert.Equal(1, response.playerProfile.Id);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((PlayerProfile?)null);

        var handler = new GetCharacterByIdHandler(mockRepo.Object);
        var query = new GetCharacterByIdQuery(999);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(response.playerProfile);
    }
}