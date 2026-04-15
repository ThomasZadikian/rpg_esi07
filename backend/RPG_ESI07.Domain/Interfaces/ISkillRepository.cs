using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllAsync();

    Task<Skill?> GetByIdAsync(int id);

    Task AddAsync(Skill entity);

    Task UpdateAsync(Skill entity);

    Task DeleteAsync(int id);
}