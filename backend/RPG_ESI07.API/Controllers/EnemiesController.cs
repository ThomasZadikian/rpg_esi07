using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RPG_ESI07.Application.Commands;
using RPG_ESI07.Application.Queries;
using RPG_ESI07.Infrastructure.Data;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EnemiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<EnemiesController> _logger;

    public EnemiesController(IMediator mediator, ILogger<EnemiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// Get all ennemies
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int Id)
    {
        var query = new GetEnemyByIdQuery(Id);
        var response = await _mediator.Send(query);

        if (response.Enemy == null)
            return NotFound(new { error = $"Enemy with ID {Id} not found" });

        return Ok(response.Enemy);
    }

    /// <summary>
    /// Create a new enemy
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEnemyCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Update an existing enemy
    /// </summary>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateEnemyCommand command)
    {
        var updateCommand = command with { Id = id };
        var response = await _mediator.Send(updateCommand);
        return Ok(response); 
    }

    /// <summary>
    /// Delete an existing enemy
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteEnemyCommand(id);
        var response = await _mediator.Send(command); 
        return Ok(response);
    }
}