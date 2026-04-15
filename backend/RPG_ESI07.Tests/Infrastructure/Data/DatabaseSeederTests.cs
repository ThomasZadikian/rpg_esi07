using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Infrastructure.Data;
using System.Text;

namespace RPG_ESI07.Tests.Infrastructure.Data;

public class DatabaseSeederTests : IDisposable
{
    private readonly AppDbContext _context;

    public DatabaseSeederTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task SeedAsync_EmptyDatabase_PopulatesAllEntities()
    {
        await DatabaseSeeder.SeedAsync(_context);

        var userCount = await _context.Users.CountAsync();
        var profileCount = await _context.PlayerProfiles.CountAsync();
        var enemyCount = await _context.Enemies.CountAsync();
        var itemCount = await _context.Items.CountAsync();
        var skillCount = await _context.Skills.CountAsync();
        var combatStatsCount = await _context.CombatStats.CountAsync();

        userCount.Should().Be(3);
        profileCount.Should().Be(3);
        enemyCount.Should().Be(8);
        itemCount.Should().Be(16);
        skillCount.Should().Be(9);
        combatStatsCount.Should().Be(3);
    }

    [Fact]
    public async Task SeedAsync_ExistingDatabase_AbortsSeeding()
    {
        _context.Users.Add(new User
        {
            Username = "PreExistingUser",
            Email = Encoding.UTF8.GetBytes("test@test.com"),
            PasswordHash = "hash",
            CreatedAt = DateTime.UtcNow,
            Role = "Player"
        });
        await _context.SaveChangesAsync();

        await DatabaseSeeder.SeedAsync(_context);

        var userCount = await _context.Users.CountAsync();
        var profileCount = await _context.PlayerProfiles.CountAsync();

        userCount.Should().Be(1);
        profileCount.Should().Be(0); // Le seeder a ignoré l'exécution complète
    }
}