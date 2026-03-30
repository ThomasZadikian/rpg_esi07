using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.PlayerInventorys;
using RPG_ESI07.Application.Queries.PlayerInventorys;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlayerInventoriesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlayerInventoriesController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllPlayerInventorysQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePlayerInventoryCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlayerInventoryCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeletePlayerInventoryCommand(id));
        return Ok(result);
    }
}
