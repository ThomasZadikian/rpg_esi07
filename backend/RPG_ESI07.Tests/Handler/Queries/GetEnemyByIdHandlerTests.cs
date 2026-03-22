using Moq;
using RPG_ESI07.Application.Queries.Enemies;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure;

namespace RPG_ESI07.Tests.Handler.Queries;

public class GetEnemyByIdHandlerTests
{
    [Fact]
    public async Task Handle_WithValidId_ReturnsEnemy()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();
        var enemy = new Enemy { Id = 1, Name = "Goblin", Type = "basic" };
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(enemy);

        var handler = new GetEnemyByIdHandler(mockRepo.Object);
        var query = new GetEnemyByIdQuery(1);


        // Act
        var response = await handler.Handle(query, CancellationToken.None); 

        // Assert
        Assert.NotNull(response.Enemy);
        Assert.Equal("Goblin", response.Enemy.Name);
        Assert.Equal("basic", response.Enemy.Type); 
        Assert.Equal(1, response.Enemy.Id);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ReturnsNull()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();
        var enemy = new Enemy { Id = 1, Name = "Orc", Type = "basic" };
        mockRepo.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Enemy?)null); 

        var handler = new GetEnemyByIdHandler(mockRepo.Object);
        var query = new GetEnemyByIdQuery(2);

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Null(response.Enemy); 
    }

}
