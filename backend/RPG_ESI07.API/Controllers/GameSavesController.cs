using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.GameSaves;
using RPG_ESI07.Application.Queries.GameSaves;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameSavesController : ControllerBase
{
    private readonly IMediator _mediator;

    public GameSavesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllGameSavesQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateGameSaveCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateGameSaveCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteGameSaveCommand(id));
        return Ok(result);
    }
}
