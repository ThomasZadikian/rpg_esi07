using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ItemsController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Get all items
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _context.Items
            .OrderBy(i => i.Type)
            .ThenBy(i => i.Price)
            .ToListAsync();

        return Ok(items);
    }

    /// <summary>
    /// Get items by type (weapon, armor, accessory, consumable)
    /// </summary>
    [HttpGet("type/{type}")]
    public async Task<IActionResult> GetByType(string type)
    {
        var items = await _context.Items
            .Where(i => i.Type.ToLower() == type.ToLower())
            .ToListAsync();

        return Ok(items);
    }
}