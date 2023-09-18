using ASE3040.Application.Features.ToDoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ISender _mediator;

    public IndexModel(ILogger<IndexModel> logger,
        ISender mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    public List<ToDoItemDto> ToDoItems { get; set; } = default!;

    public async Task OnGetAsync()
    {
        ToDoItems = await _mediator.Send(new GetToDoItemsQuery());
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await HttpContext.SignOutAsync();
        return RedirectToPage();
    }

}

