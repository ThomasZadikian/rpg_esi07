using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.Characters;
using RPG_ESI07.Application.Queries.Characters;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerProfilesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayerProfilesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediator.Send(new GetAllCharactersQuery());
        return Ok(response.playerProfile);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediator.Send(new GetCharacterByIdQuery(id));
        if (response.playerProfile == null)
            return NotFound(new { error = $"PlayerProfile with ID {id} not found" });
        return Ok(response.playerProfile);
    }

    [HttpGet("by-level")]
    public async Task<IActionResult> GetByLevel([FromQuery] int level)
    {
        var response = await _mediator.Send(new GetCharactersByLevelQuery(level));
        return Ok(response.playerProfiles);
    }

    [HttpGet("by-speed")]
    public async Task<IActionResult> GetBySpeed()
    {
        var response = await _mediator.Send(new GetCharactersBySpeedQuery());
        return Ok(response.playerProfile);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCharacterCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCharactersCommand command)
    {
        var updateCmd = command with { UserId = id };
        var response = await _mediator.Send(updateCmd);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _mediator.Send(new DeleteCharacterCommand(id));
        return Ok(response);
    }
}