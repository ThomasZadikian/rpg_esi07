using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.BestiaryUnlocks;
using RPG_ESI07.Application.Queries.BestiaryUnlocks;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BestiaryUnlocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public BestiaryUnlocksController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllBestiaryUnlocksQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBestiaryUnlockCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBestiaryUnlockCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteBestiaryUnlockCommand(id));
        return Ok(result);
    }
}
