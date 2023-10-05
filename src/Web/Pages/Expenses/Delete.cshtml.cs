using ASE3040.Application.Features.TodoLists.Commands.Delete;
using ASE3040.Application.Features.TodoLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Expenses
{
    public class DeleteModel : PageModel
    {
        private readonly ISender _mediator;

        public DeleteModel(ISender mediator)
        {
            _mediator = mediator;
        }

        // The ID of the ToDo item.
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        // This is your property that you'll bind the fetched ToDoItem to.
        public ToDoListDto ToDoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id; // Set the Id property so it can be used in OnPostAsync.

            ToDoItem = await _mediator.Send(new GetToDoList { Id = id });
            if (ToDoItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new DeleteToDoListCommand()
            {
                Id = Id
            };

            var result = await _mediator.Send(request);

            return RedirectToPage("./Index");
        }
    }
}