using ASE3040.Application.Features.ToDoItems.Commands.Delete;
using ASE3040.Application.Features.ToDoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.ToDoItems
{
    public class DeleteModel : PageModel
    {
        private readonly ISender _mediator;

        public DeleteModel(ISender mediator)
        {
            _mediator = mediator;
        }
        
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        
        public ToDoItemDto ToDoItem { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Id = id;

            ToDoItem = await _mediator.Send(new GetToDoItem { Id = id });
            if (ToDoItem == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var request = new DeleteToDoItemCommand()
            {
                Id = Id
            };

            var result = await _mediator.Send(request);

            return RedirectToPage("./Index");
        }
    }
}