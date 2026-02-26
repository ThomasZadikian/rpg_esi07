using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnemiesController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<EnemiesController> _logger;

    public EnemiesController(AppDbContext context, ILogger<EnemiesController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Get all enemies
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var enemies = await _context.Enemies
            .OrderBy(e => e.Type)
            .ThenBy(e => e.MaxHP)
            .ToListAsync();

        return Ok(enemies);
    }

    /// <summary>
    /// Get enemy by ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var enemy = await _context.Enemies.FindAsync(id);

        if (enemy == null)
        {
            return NotFound(new { error = $"Enemy with ID {id} not found" });
        }

        return Ok(enemy);
    }

    /// <summary>
    /// Get enemies by type (basic, miniboss, boss)
    /// </summary>
    [HttpGet("type/{type}")]
    public async Task<IActionResult> GetByType(string type)
    {
        if (!new[] { "basic", "miniboss", "boss" }.Contains(type.ToLower()))
        {
            return BadRequest(new { error = "Invalid enemy type. Must be: basic, miniboss, or boss" });
        }

        var enemies = await _context.Enemies
            .Where(e => e.Type.ToLower() == type.ToLower())
            .ToListAsync();

        return Ok(enemies);
    }
}