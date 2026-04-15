using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IPlayerSkillRepository
{
    Task<List<PlayerSkill>> GetAllAsync();

    Task<PlayerSkill?> GetByIdAsync(int id);

    Task AddAsync(PlayerSkill entity);

    Task UpdateAsync(PlayerSkill entity);

    Task DeleteAsync(int id);
}