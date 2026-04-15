using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Domain.Entities;
using RPG_ESI07.Infrastructure.Data;
using RPG_ESI07.Infrastructure.Repository;

namespace RPG_ESI07.Tests.Infrastructure.Repository;

public class GameSaveRepositoryTests : IDisposable
{
    private readonly AppDbContext _context;
    private readonly GameSaveRepository _repository;

    public GameSaveRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new AppDbContext(options);
        _repository = new GameSaveRepository(_context);
    }

    public void Dispose()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllRecords_OrderedById()
    {
        var gameSaves = new List<GameSave>
        {
            new GameSave { Id = 2, PlayerId = 1, CurrentZone = "Zone2" },
            new GameSave { Id = 1, PlayerId = 1, CurrentZone = "Zone1" }
        };
        await _context.GameSaves.AddRangeAsync(gameSaves);
        await _context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        result.Should().HaveCount(2);
        result[0].Id.Should().Be(1);
        result[1].Id.Should().Be(2);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsRecord_WhenIdExists()
    {
        var gameSave = new GameSave { Id = 5, PlayerId = 10, CurrentZone = "Tutorial" };
        await _context.GameSaves.AddAsync(gameSave);
        await _context.SaveChangesAsync();

        var result = await _repository.GetByIdAsync(5);

        result.Should().NotBeNull();
        result!.Id.Should().Be(5);
        result.PlayerId.Should().Be(10);
        result.CurrentZone.Should().Be("Tutorial");
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenIdDoesNotExist()
    {
        var result = await _repository.GetByIdAsync(999);

        result.Should().BeNull();
    }

    [Fact]
    public async Task AddAsync_InsertsRecordAndSavesChanges()
    {
        var newSave = new GameSave { Id = 10, PlayerId = 5, CurrentZone = "Forest" };

        await _repository.AddAsync(newSave);

        var dbRecord = await _context.GameSaves.FirstOrDefaultAsync(x => x.Id == 10);
        dbRecord.Should().NotBeNull();
        dbRecord!.PlayerId.Should().Be(5);
        dbRecord.CurrentZone.Should().Be("Forest");
    }

    [Fact]
    public async Task UpdateAsync_ModifiesRecordAndSavesChanges()
    {
        var save = new GameSave { Id = 1, PlayerId = 1, CurrentZone = "Start" };
        await _context.GameSaves.AddAsync(save);
        await _context.SaveChangesAsync();

        _context.Entry(save).State = EntityState.Detached;

        var updatedSave = new GameSave { Id = 1, PlayerId = 1, CurrentZone = "EndGame" };
        await _repository.UpdateAsync(updatedSave);

        var dbRecord = await _context.GameSaves.FindAsync(1);
        dbRecord.Should().NotBeNull();
        dbRecord!.CurrentZone.Should().Be("EndGame");
    }

    [Fact]
    public async Task DeleteAsync_RemovesRecord_WhenIdExists()
    {
        var save = new GameSave { Id = 1, PlayerId = 1, CurrentZone = "Start" };
        await _context.GameSaves.AddAsync(save);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(1);

        var dbRecord = await _context.GameSaves.FindAsync(1);
        dbRecord.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_DoesNothing_WhenIdDoesNotExist()
    {
        var save = new GameSave { Id = 1, PlayerId = 1, CurrentZone = "Start" };
        await _context.GameSaves.AddAsync(save);
        await _context.SaveChangesAsync();

        await _repository.DeleteAsync(999);

        var remainingCount = await _context.GameSaves.CountAsync();
        remainingCount.Should().Be(1);
    }
}