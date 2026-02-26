using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Infrastructure.Data;
using Xunit;

namespace RPG_ESI07.Tests.Infrastructure;

public class AppDbContextTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly AppDbContext _context;

    public AppDbContextTests()
    {
        // Instanciation et maintien de la connexion SQLite en mémoire
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new AppDbContext(options);

        // Matérialisation obligatoire du schéma et des contraintes SQL
        _context.Database.EnsureCreated();
    }

    [Fact]
    public async Task CanAddAndRetrieveUser()
    {
        // Arrange
        var user = new User
        {
            Username = "testuser",
            Email = System.Text.Encoding.UTF8.GetBytes("test@example.com"),
            PasswordHash = "hash123",
            MfaEnabled = false,
            FailedLoginAttempts = 1
        };

        // Act
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var retrieved = await _context.Users
            .FirstOrDefaultAsync(u => u.Username == "testuser");

        // Assert
        retrieved.Should().NotBeNull();
        retrieved!.Username.Should().Be("testuser");
        retrieved.MfaEnabled.Should().BeFalse();
    }

    [Fact]
    public async Task UserUsernameIsUnique()
    {
        // Arrange
        var user1 = new User { Username = "duplicate", Email = new byte[0], PasswordHash = "hash1" };
        var user2 = new User { Username = "duplicate", Email = new byte[0], PasswordHash = "hash2" };

        _context.Users.Add(user1);
        await _context.SaveChangesAsync();

        // Act
        _context.Users.Add(user2);
        Func<Task> act = async () => await _context.SaveChangesAsync();

        // Assert
        await act.Should().ThrowAsync<DbUpdateException>();
    }

    [Fact]
    public async Task PlayerProfileCascadeDeletesWithUser()
    {
        // Arrange
        var user = new User { Username = "cascade", Email = new byte[0], PasswordHash = "hash" };
        var profile = new PlayerProfile { User = user, CharacterName = "Hero" };

        _context.Users.Add(user);
        _context.PlayerProfiles.Add(profile);
        await _context.SaveChangesAsync();

        // Act
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        // Assert
        var remainingProfiles = await _context.PlayerProfiles.CountAsync();
        remainingProfiles.Should().Be(0);
    }

    [Fact]
    public async Task CanCreateEnemyWithConstraints()
    {
        // Arrange
        var enemy = new Enemy
        {
            Name = "TestEnemy",
            Type = "basic",
            MaxHP = 100,
            Strength = 10,
            Intelligence = 5,
            Speed = 8,
            PhysicalResistance = 1.0f,
            MagicalResistance = 1.0f,
            ExperienceReward = 50,
            GoldReward = 25
        };

        // Act
        _context.Enemies.Add(enemy);
        await _context.SaveChangesAsync();

        // Assert
        var retrieved = await _context.Enemies.FirstOrDefaultAsync(e => e.Name == "TestEnemy");
        retrieved.Should().NotBeNull();
        retrieved!.Type.Should().Be("basic");
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
        _connection.Dispose();
    }
}