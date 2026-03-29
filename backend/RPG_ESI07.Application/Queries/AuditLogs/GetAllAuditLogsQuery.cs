using MediatR;
using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Application.Queries.AuditLogs;

public record GetAllAuditLogsQuery : IRequest<GetAllAuditLogsResponse>;

public record GetAllAuditLogsResponse(List<AuditLog> Items);
