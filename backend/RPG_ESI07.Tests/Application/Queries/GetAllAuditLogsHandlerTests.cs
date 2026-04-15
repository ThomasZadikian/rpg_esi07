using FluentAssertions;
using Moq;
using RPG_ESI07.Application.Queries.AuditLogs;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RPG_ESI07.Tests.Application.Queries;

public class GetAllAuditLogsHandlerTests
{
    [Fact]
    public async Task Handle_ReturnsAllItems()
    {
        var mockRepo = new Mock<IAuditLogRepository>();
        var items = new List<AuditLog> { new() { Id = 1 }, new() { Id = 2 } };
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(items);
        var handler = new GetAllAuditLogsHandler(mockRepo.Object);

        var result = await handler.Handle(new GetAllAuditLogsQuery(), CancellationToken.None);

        result.Items.Should().HaveCount(2);
    }

    [Fact]
    public async Task Handle_EmptyList_ReturnsEmptyResponse()
    {
        var mockRepo = new Mock<IAuditLogRepository>();
        mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<AuditLog>());
        var handler = new GetAllAuditLogsHandler(mockRepo.Object);

        var result = await handler.Handle(new GetAllAuditLogsQuery(), CancellationToken.None);

        result.Items.Should().BeEmpty();
    }
}