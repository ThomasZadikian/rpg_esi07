using Moq;
using RPG_ESI07.Application.Commands.Enemies;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Handler.Command; 

public class CreateEnemyHandlerTest
{
    [Fact] 
    public async Task Handler_WithValidCommand_CreateEnemy()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();
        mockRepo.Setup(r => r.AddAsync(It.IsAny<Enemy>()))
            .Returns(Task.CompletedTask);

        var handler = new CreateEnemyHandler(mockRepo.Object);
        var command = new CreateEnemyCommand(
            Name: "Dragon",
            Type: "Boss",
            MaxHP: 100,
            Strength: 50,
            Intelligence: 40,
            Speed: 30,
            PhysicalResistance: 1.5f,
            MagicalResistance: 1.0f,
            ExperienceReward: 1000,
            GoldReward: 500,
            Description: "A mighty dragon"
            );

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        Assert.Equal("Enemy created successfully", response.Message);
        mockRepo.Verify(r => r.AddAsync(It.IsAny<Enemy>()), Times.Once()); 



    }
}