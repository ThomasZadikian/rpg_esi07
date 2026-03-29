using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IGameSaveRepository
{
    Task<List<GameSave>> GetAllAsync();
    Task<GameSave?> GetByIdAsync(int id);
    Task AddAsync(GameSave entity);
    Task UpdateAsync(GameSave entity);
    Task DeleteAsync(int id);
}
