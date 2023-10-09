using ASE3040.Application.Features.ToDoItems.Commands.Create;
using ASE3040.Application.Features.ToDoItems.Queries;
using ASE3040.Application.Features.TodoLists.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ToDoItemDto = ASE3040.Application.Features.ToDoItems.Queries.ToDoItemDto;

namespace ASE3040.Web.Pages.TodoItem
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
        {
            _mediator = mediator;
        }
        public IList<ToDoItemDto> ToDoItems { get;set; } = default!;
        [BindProperty] public string? Title { get; set; } = default!;
        [BindProperty] public string? Note { get; set; } = default!;
        [BindProperty] public int ToDoListId { get; set; }
        [BindProperty] public DateTime DateTime { get; set; } = default!;
        public async Task OnGetAsync()
        {
            ToDoItems = await _mediator.Send(new GetToDoItemsQuery());
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var request = new CreateToDoItemCommand()
            {
                ToDoListId = ToDoListId,
                Title = Title,
                Note = Note,
                DateTime = DateTime
            };

            var result = await _mediator.Send(request);

            return RedirectToPage("./Index");
        }
    }
}