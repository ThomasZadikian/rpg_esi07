using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IPlayerProfileRepository
{
    Task<List<PlayerProfile>> GetAllAsync();

    Task<PlayerProfile?> GetByIdAsync(int id);

    Task<List<PlayerProfile>> GetByLevelAsync(int level);

    Task<List<PlayerProfile>> GetBySpeedAsync();

    Task AddAsync(PlayerProfile profile);

    Task UpdateAsync(PlayerProfile profile);

    Task DeleteAsync(int id);
}