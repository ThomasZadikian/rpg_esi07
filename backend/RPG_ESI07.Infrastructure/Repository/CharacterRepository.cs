using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Domain.Interfaces;
using RPG_ESI07.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_ESI07.Infrastructure.Repository;

public class CharacterRepository : IPlayerProfileRepository
{
    private readonly AppDbContext _context;

    public CharacterRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PlayerProfile?> GetByIdAsync(int id)
    {
        return await _context.PlayerProfiles.FindAsync(id);
    }

    public async Task<List<PlayerProfile>> GetAllAsync()
    {
        return await _context.PlayerProfiles
            .OrderBy(e => e.Level)
            .ThenBy(e => e.Experience)
            .ToListAsync();
    }

    public async Task<List<PlayerProfile>> GetBySpeedAsync()
    {
        return await _context.PlayerProfiles.
            OrderBy(e => e.Speed).
            ToListAsync(); 
    }

    public async Task<List<PlayerProfile>> GetByLevelAsync(int level)
    {
        return await _context.PlayerProfiles.
            OrderBy(e => e.Level)
            .ToListAsync();  
    }

    public async Task AddAsync(PlayerProfile character)
    {
        _context.PlayerProfiles.Add(character);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(PlayerProfile character)
    {
        _context.PlayerProfiles.Update(character);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var enemy = await _context.Enemies.FindAsync(id);
        if (enemy != null)
        {
            _context.Enemies.Remove(enemy);
            await _context.SaveChangesAsync();
        }
    }
}