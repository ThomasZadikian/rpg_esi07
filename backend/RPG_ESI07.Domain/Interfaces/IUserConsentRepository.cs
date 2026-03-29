using RPG_ESI07.Domain.Entities;

namespace RPG_ESI07.Domain.Interfaces;

public interface IUserConsentRepository
{
    Task<List<UserConsent>> GetAllAsync();
    Task<UserConsent?> GetByIdAsync(int id);
    Task AddAsync(UserConsent entity);
    Task UpdateAsync(UserConsent entity);
    Task DeleteAsync(int id);
}
