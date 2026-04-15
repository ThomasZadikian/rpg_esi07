using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface ICombatStatsRepository
{
    Task<List<CombatStats>> GetAllAsync();

    Task<CombatStats?> GetByIdAsync(int id);

    Task AddAsync(CombatStats entity);

    Task UpdateAsync(CombatStats entity);

    Task DeleteAsync(int id);
}