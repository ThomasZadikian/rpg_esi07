using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.Infrastructure.Repository;

public class CombatStatsRepository : ICombatStatsRepository
{
    private readonly AppDbContext _context;

    public CombatStatsRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<CombatStats?> GetByIdAsync(int id)
    {
        return await _context.Set<CombatStats>().FindAsync(id);
    }

    public async Task<List<CombatStats>> GetAllAsync()
    {
        return await _context.Set<CombatStats>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(CombatStats entity)
    {
        _context.Set<CombatStats>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CombatStats entity)
    {
        _context.Set<CombatStats>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<CombatStats>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<CombatStats>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
