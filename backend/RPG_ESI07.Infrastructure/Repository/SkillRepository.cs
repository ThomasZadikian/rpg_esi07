using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.Infrastructure.Repository;

public class SkillRepository : ISkillRepository
{
    private readonly AppDbContext _context;

    public SkillRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Skill?> GetByIdAsync(int id)
    {
        return await _context.Set<Skill>().FindAsync(id);
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _context.Set<Skill>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(Skill entity)
    {
        _context.Set<Skill>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Skill entity)
    {
        _context.Set<Skill>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<Skill>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<Skill>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}