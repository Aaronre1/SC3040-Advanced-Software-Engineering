using Microsoft.AspNetCore.Mvc;
using ASE3040.Application.Features.ToDoItems.Commands.Create;
using MediatR;

[Route("api/[controller]")]
[ApiController]
public class ToDoItemsController : ControllerBase
{
    private readonly IMediator _mediator;

    public ToDoItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateToDoItem(CreateToDoItemCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.Succeeded)
            return Ok(new { success = true });

        return BadRequest(new { success = false, errors = result.Errors });
    }
}