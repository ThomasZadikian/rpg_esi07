using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly ILogger<HealthController> _logger;

    public HealthController(AppDbContext context, ILogger<HealthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Basic health check endpoint
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "healthy",
            service = "RPG_ESI07 API",
            version = "1.0.0",
            timestamp = DateTime.UtcNow,
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        });
    }

    /// <summary>
    /// Database connectivity health check
    /// </summary>
    [HttpGet("db")]
    public async Task<IActionResult> CheckDatabase()
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();
            if (!canConnect)
            {
                _logger.LogError("Database connection failed");
                return StatusCode(503, new
                {
                    status = "unhealthy",
                    error = "Cannot connect to database",
                    timestamp = DateTime.UtcNow
                });
            }

            // Get counts
            var userCount = await _context.Users.CountAsync();
            var enemyCount = await _context.Enemies.CountAsync();
            var itemCount = await _context.Items.CountAsync();

            return Ok(new
            {
                status = "healthy",
                database = "connected",
                statistics = new
                {
                    users = userCount,
                    enemies = enemyCount,
                    items = itemCount
                },
                timestamp = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database health check failed");
            return StatusCode(503, new
            {
                status = "unhealthy",
                error = ex.Message,
                timestamp = DateTime.UtcNow
            });
        }
    }
}