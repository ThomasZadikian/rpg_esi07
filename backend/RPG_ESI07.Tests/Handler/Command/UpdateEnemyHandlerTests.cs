using Moq;
using Xunit;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using AutoMapper;
using RPG_ESI07.API.Controllers;

namespace RPG_ESI07.Tests.Handlers.Commands;

public class UpdateEnemyHandlerTests
{
    [Fact]
    public async Task Handle_WithValidCommand_UpdatesEnemy()
    {
        // Arrange
        var existingEnemy = new Enemy
        {
            Id = 1,
            Name = "Goblin",
            Type = "basic",
            MaxHP = 30,
            Strength = 10,
            Intelligence = 5,
            Speed = 15,
            PhysicalResistance = 1.0f,
            MagicalResistance = 1.0f,
            ExperienceReward = 50,
            GoldReward = 25
        };

        var mockRepo = new Mock<IEnemyRepository>();
        var mockMapper = new Mock<IMapper>();

        mockMapper.Setup(m => m.Map(It.IsAny<UpdateEnemyCommand>(), It.IsAny<Enemy>()))
            .Callback<UpdateEnemyCommand, Enemy>((cmd, enemy) =>
            {
                enemy.Name = cmd.Name;
                enemy.Type = cmd.Type;
                enemy.MaxHP = cmd.MaxHP;
                enemy.Strength = cmd.Strength;
                enemy.Intelligence = cmd.Intelligence;
                enemy.Speed = cmd.Speed;
                enemy.PhysicalResistance = cmd.PhysicalResistance;
                enemy.MagicalResistance = cmd.MagicalResistance;
                enemy.ExperienceReward = cmd.ExperienceReward;
                enemy.GoldReward = cmd.GoldReward;
                enemy.Description = cmd.Description;
            });

        mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(existingEnemy);
        mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Enemy>()))
            .Returns(Task.CompletedTask);

        var handler = new UpdateEnemyHandler(mockRepo.Object, mockMapper.Object);
        var command = new UpdateEnemyCommand(
            Id: 1,
            Name: "Stronger Goblin",
            Type: "miniboss",
            MaxHP: 50,
            Strength: 20,
            Intelligence: 10,
            Speed: 20,
            PhysicalResistance: 1.2f,
            MagicalResistance: 1.0f,
            ExperienceReward: 100,
            GoldReward: 50,
            Description: "An upgraded goblin"
        );

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal("Enemy updated successfully", response.Message);
        mockRepo.Verify(r => r.GetByIdAsync(1), Times.Once);
        mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Enemy>()), Times.Once);
    }

    [Fact]
    public async Task Handle_WithInvalidId_ThrowsException()
    {
        // Arrange
        var mockRepo = new Mock<IEnemyRepository>();
        var mockMapper = new Mock<IMapper>(); 
        mockRepo.Setup(r => r.GetByIdAsync(999))
            .ReturnsAsync((Enemy?)null);
        mockMapper.Setup(m => m.Map(It.IsAny<UpdateEnemyCommand>(), It.IsAny<Enemy>()))
            .Callback<UpdateEnemyCommand, Enemy>((cmd, enemy) =>
                {
                    enemy.Name = cmd.Name;
                    enemy.Type = cmd.Type;
                    enemy.MaxHP = cmd.MaxHP;
                    enemy.Strength = cmd.Strength;
                    enemy.Intelligence = cmd.Intelligence;
                    enemy.Speed = cmd.Speed;
                    enemy.PhysicalResistance = cmd.PhysicalResistance;
                    enemy.MagicalResistance = cmd.MagicalResistance;
                    enemy.ExperienceReward = cmd.ExperienceReward;
                    enemy.GoldReward = cmd.GoldReward;
                    enemy.Description = cmd.Description;
                });

        var handler = new UpdateEnemyHandler(mockRepo.Object, mockMapper.Object);
        var command = new UpdateEnemyCommand(
            Id: 999,
            Name: "Ghost",
            Type: "basic",
            MaxHP: 10,
            Strength: 5,
            Intelligence: 5,
            Speed: 10,
            PhysicalResistance: 1.0f,
            MagicalResistance: 1.0f,
            ExperienceReward: 10,
            GoldReward: 5,
            Description: null
        );

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(
            () => handler.Handle(command, CancellationToken.None)
        );
    }
}