using Moq;
using Xunit;
using RPG_ESI07.Application.Queries.Characters;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Queries;

public class GetAllCharactersHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllCharacters()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<PlayerProfile>
            {
                new PlayerProfile { Id = 1, UserId = 1, CharacterName = "Hero", Level = 5, Strength = 15 },
                new PlayerProfile { Id = 2, UserId = 2, CharacterName = "Warrior", Level = 8, Strength = 20 }
            });

        var handler = new GetAllCharactersHandler(mockRepo.Object);
        var query = new GetAllCharactersQuery();

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, response.playerProfile.Count);
        Assert.Equal("Hero", response.playerProfile[0].CharacterName);
        Assert.Equal("Warrior", response.playerProfile[1].CharacterName);
    }

    [Fact]
    public async Task Handle_WithEmptyList_ReturnsEmpty()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<PlayerProfile>());

        var handler = new GetAllCharactersHandler(mockRepo.Object);
        var query = new GetAllCharactersQuery();

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(response.playerProfile);
    }
}