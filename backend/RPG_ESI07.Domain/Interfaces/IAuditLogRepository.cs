using RPG_ESI07.Domain.Entities;
namespace RPG_ESI07.Domain.Interfaces;

public interface IAuditLogRepository
{
    Task<List<AuditLog>> GetAllAsync();
    Task<AuditLog?> GetByIdAsync(int id);
    Task AddAsync(AuditLog auditLog);
    Task DeleteAsync(int id);
}