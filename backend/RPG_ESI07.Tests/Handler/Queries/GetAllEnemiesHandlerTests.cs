using Moq;
using RPG_ESI07.Application.Queries.Enemies;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handler.Queries; 

public class GetAllEnemiesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllEnemies()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();
        var enemies = new List<Enemy>
        {
            new Enemy { Id = 1, Name = "Orc", Type = "basics"},
            new Enemy { Id = 2, Name = "Gobelin", Type = "miniboss" }
        }; 

        mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(enemies);

        var handler = new GetAllEnemiesHandler(mockRepo.Object);
        var query = new GetAllEnemiesQuery();

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert 
        Assert.Equal(2, response.Enemies.Count);
        Assert.Equal("Orc", response.Enemies[0].Name);
        Assert.Equal("Gobelin", response.Enemies[1].Name); 

    }
}
