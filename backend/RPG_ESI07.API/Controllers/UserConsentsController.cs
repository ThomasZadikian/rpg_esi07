using MediatR;
using Microsoft.AspNetCore.Mvc;
using RPG_ESI07.Application.Commands.UserConsents;
using RPG_ESI07.Application.Queries.UserConsents;

namespace RPG_ESI07.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserConsentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserConsentsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllUserConsentsQuery());
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserConsentCommand command)
    {
        var result = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserConsentCommand command)
    {
        if (id != command.Id) return BadRequest("Id mismatch");
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteUserConsentCommand(id));
        return Ok(result);
    }
}
