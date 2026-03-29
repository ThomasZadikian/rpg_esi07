using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IPlayerInventoryRepository
{
    Task<List<PlayerInventory>> GetAllAsync();
    Task<PlayerInventory?> GetByIdAsync(int id);
    Task AddAsync(PlayerInventory entity);
    Task UpdateAsync(PlayerInventory entity);
    Task DeleteAsync(int id);
}
