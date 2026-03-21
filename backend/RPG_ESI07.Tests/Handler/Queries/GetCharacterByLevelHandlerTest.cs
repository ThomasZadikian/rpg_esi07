using Moq;
using Xunit;
using RPG_ESI07.Application.Queries;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Queries;

public class GetCharactersByLevelHandlerTests
{
    [Fact]
    public async Task Handle_WithValidLevel_ReturnsCharacters()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetByLevelAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<PlayerProfile>
            {
                new PlayerProfile { Id = 1, CharacterName = "Hero", Level = 5 },
                new PlayerProfile { Id = 2, CharacterName = "Paladin", Level = 5 }
            });

        var handler = new GetCharactersByLevelHandler(mockRepo.Object);
        var query = new GetCharactersByLevelQuery(5);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, response.playerProfiles.Count);
    }

    [Fact]
    public async Task Handle_WithNoCharactersAtLevel_ReturnsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetByLevelAsync(It.IsAny<int>()))
            .ReturnsAsync(new List<PlayerProfile>());

        var handler = new GetCharactersByLevelHandler(mockRepo.Object);
        var query = new GetCharactersByLevelQuery(50);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(response.playerProfiles);
    }
}