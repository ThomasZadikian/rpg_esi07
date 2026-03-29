using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.CombatStatss;
using RPG_ESI07.Application.Queries.CombatStatss;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CombatStatsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CombatStatsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCombatStatssQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCombatStatsCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCombatStatsCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCombatStatsCommand(id));
        return Ok(result);
    }
}
