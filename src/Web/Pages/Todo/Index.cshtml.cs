using ASE3040.Application.Features.TodoLists.Commands.Create;
using ASE3040.Application.Features.TodoLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public IList<ToDoListDto> ToDoLists { get;set; } = default!;
        [BindProperty] public string? Title { get; set; } = default!;
        
        public async Task OnGetAsync()
        {
            ToDoLists = await _mediator.Send(new GetToDoLists());
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = new CreateToDoListCommand()
            {
                Title = Title
            };

            var result = await _mediator.Send(request);

            return RedirectToPage("./Index");
        }
    }
}