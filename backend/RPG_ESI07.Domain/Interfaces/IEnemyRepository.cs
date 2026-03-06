using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IEnemyRepository
{
    Task<List<Enemy>> GetAllAsync();
    Task<Enemy?> GetByIdAsync(int id);
    Task<List<Enemy>> GetByTypeAsync(string type);
    Task AddAsync(Enemy enemy);
    Task UpdateAsync(Enemy enemy);
    Task DeleteAsync(int id);
}