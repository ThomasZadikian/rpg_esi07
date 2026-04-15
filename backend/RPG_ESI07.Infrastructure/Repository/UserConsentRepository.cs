using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.Infrastructure.Repository;

public class UserConsentRepository : IUserConsentRepository
{
    private readonly AppDbContext _context;

    public UserConsentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<UserConsent?> GetByIdAsync(int id)
    {
        return await _context.Set<UserConsent>().FindAsync(id);
    }

    public async Task<List<UserConsent>> GetAllAsync()
    {
        return await _context.Set<UserConsent>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(UserConsent entity)
    {
        _context.Set<UserConsent>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserConsent entity)
    {
        _context.Set<UserConsent>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<UserConsent>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<UserConsent>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}