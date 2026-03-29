using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Queries.AuditLogs;
using RPG_ESI07.Application.Queries.BestiaryUnlocks;
using RPG_ESI07.Application.Queries.Items;
using RPG_ESI07.Application.Queries.Skills;
using RPG_ESI07.Application.Queries.Users;
using RPG_ESI07.Application.Queries.GameSaves;
using RPG_ESI07.Application.Queries.PlayerSkills;
using RPG_ESI07.Application.Queries.UserConsents;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;

namespace RPG_ESI07.Tests.Application.Queries;

public class GetAllAuditLogsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllItems()
    {
        // Arrange
        var mockRepo = new Mock<IAuditLogRepository>();
        var items = new List<AuditLog> { new() { Id = 1 }, new() { Id = 2 } };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        var handler = new GetAllAuditLogsHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllAuditLogsQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_EmptyList_ReturnsEmptyResponse()
    {
        // Arrange
        var mockRepo = new Mock<IAuditLogRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<AuditLog>());
        var handler = new GetAllAuditLogsHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllAuditLogsQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().BeEmpty();
    }
}

public class GetAllItemsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllItems()
    {
        // Arrange
        var mockRepo = new Mock<IItemRepository>();
        var items = new List<Item> { new() { Id = 1, Name = "Sword" }, new() { Id = 2, Name = "Shield" } };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        var handler = new GetAllItemsHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllItemsQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(2);
        result.Items[0].Name.Should().Be("Sword");
    }
}

public class GetAllSkillsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllSkills()
    {
        // Arrange
        var mockRepo = new Mock<ISkillRepository>();
        var items = new List<Skill> { new() { Id = 1, Name = "Fireball" } };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        var handler = new GetAllSkillsHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllSkillsQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
    }
}

public class GetAllUsersHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllUsers()
    {
        // Arrange
        var mockRepo = new Mock<IUserRepository>();
        var items = new List<User> { new() { Id = 1, Username = "player1" } };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        var handler = new GetAllUsersHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().HaveCount(1);
    }
}

public class GetAllGameSavesHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllSaves()
    {
        // Arrange
        var mockRepo = new Mock<IGameSaveRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<GameSave>());
        var handler = new GetAllGameSavesHandler(mockRepo.Object);

        // Act
        var result = await handler.Handle(new GetAllGameSavesQuery(), CancellationToken.None);

        // Assert
        result.Items.Should().BeEmpty();
    }
}
