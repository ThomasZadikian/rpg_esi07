using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IItemRepository
{
    Task<List<Item>> GetAllAsync();

    Task<Item?> GetByIdAsync(int id);

    Task AddAsync(Item entity);

    Task UpdateAsync(Item entity);

    Task DeleteAsync(int id);
}