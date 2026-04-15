using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.Infrastructure.Repository;

public class GameSaveRepository : IGameSaveRepository
{
    private readonly AppDbContext _context;

    public GameSaveRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<GameSave?> GetByIdAsync(int id)
    {
        return await _context.Set<GameSave>().FindAsync(id);
    }

    public async Task<List<GameSave>> GetAllAsync()
    {
        return await _context.Set<GameSave>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(GameSave entity)
    {
        _context.Set<GameSave>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(GameSave entity)
    {
        _context.Set<GameSave>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<GameSave>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<GameSave>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}