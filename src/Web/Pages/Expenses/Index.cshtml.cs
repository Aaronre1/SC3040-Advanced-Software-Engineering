using ASE3040.Application.Features.TodoLists.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ASE3040.Web.Pages.Todo
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
        {
            _mediator = mediator;
        }

        public IList<ToDoListDto> ToDoLists { get; set; } = default!;

        public async Task OnGetAsync()
        {
            ToDoLists = await _mediator.Send(new GetToDoLists());
        }
    }

    public class Temp
    {
    }
}