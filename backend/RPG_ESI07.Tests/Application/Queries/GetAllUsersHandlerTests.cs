using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Queries.Users;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RPG_ESI07.Tests.Application.Queries;

public class GetAllUsersHandlerTests
{
    private readonly Mock<IUserRepository> _mockRepo;
    private readonly GetAllUsersHandler _handler;

    public GetAllUsersHandlerTests()
    {
        _mockRepo = new Mock<IUserRepository>(MockBehavior.Strict);
        _handler = new GetAllUsersHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_ReturnsAllItems()
    {
        var items = new List<User> { new() { Id = 1 }, new() { Id = 2 } };
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);

        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        result.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_EmptyList_ReturnsEmptyResponse()
    {
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<User>());

        var result = await _handler.Handle(new GetAllUsersQuery(), CancellationToken.None);

        result.Items.Should().BeEmpty();
    }
}