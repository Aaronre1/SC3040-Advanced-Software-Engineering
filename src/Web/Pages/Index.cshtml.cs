using ASE3040.Application.Features.ToDoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages;

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

}

