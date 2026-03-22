using Moq;
using Xunit;
using RPG_ESI07.Application.Queries.Characters;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handlers.Queries;

public class GetCharactersBySpeedHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsCharactersOrderedBySpeed()
    {
        // Arrange
        var mockRepo = new Mock<IPlayerProfileRepository>();
        mockRepo.Setup(r => r.GetBySpeedAsync())
            .ReturnsAsync(new List<PlayerProfile>
            {
                new PlayerProfile { Id = 1, CharacterName = "Slow", Speed = 5 },
                new PlayerProfile { Id = 2, CharacterName = "Fast", Speed = 20 }
            });

        var handler = new GetCharactersBySpeedHandler(mockRepo.Object);
        var query = new GetCharactersBySpeedQuery();

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Equal(2, response.playerProfile.Count);
        Assert.Equal(5, response.playerProfile[0].Speed);
        Assert.Equal(20, response.playerProfile[1].Speed);
    }
}