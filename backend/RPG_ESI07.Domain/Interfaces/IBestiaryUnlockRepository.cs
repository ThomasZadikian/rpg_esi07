using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IBestiaryUnlockRepository
{
    Task<List<BestiaryUnlock>> GetAllAsync();

    Task<BestiaryUnlock?> GetByIdAsync(int id);

    Task AddAsync(BestiaryUnlock entity);

    Task UpdateAsync(BestiaryUnlock entity);

    Task DeleteAsync(int id);
}