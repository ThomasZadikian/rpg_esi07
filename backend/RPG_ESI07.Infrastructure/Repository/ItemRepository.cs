using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.Infrastructure.Repository;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        return await _context.Set<Item>().FindAsync(id);
    }

    public async Task<List<Item>> GetAllAsync()
    {
        return await _context.Set<Item>()
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task AddAsync(Item entity)
    {
        _context.Set<Item>().Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Item entity)
    {
        _context.Set<Item>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Set<Item>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<Item>().Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}