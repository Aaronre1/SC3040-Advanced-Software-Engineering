using ASE3040.Application.Features.ToDoItems.Commands.Edit;
using ASE3040.Application.Features.ToDoItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.TodoItem
{
    public class EditModel : PageModel
    {
        private readonly ISender _mediator;
        private readonly ILogger<EditModel> _logger;
        public EditModel(ISender mediator, ILogger<EditModel> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [BindProperty]
        public EditToDoItemCommand Command { get; set; } = new EditToDoItemCommand();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var toDoItem = await _mediator.Send(new GetToDoItem { Id = id });
            if (toDoItem == null)
            {
                return NotFound();
            }

            Command.Id = toDoItem.Id;
            Command.Title = toDoItem.Title;
            Command.Note = toDoItem.Note;
            Command.Done = toDoItem.Done;
            Command.DateTime = toDoItem.DateTime;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            _logger.LogInformation($"Attempting to edit item with ID: {Command.Id} and new Title: {Command.Title}");
            var result = await _mediator.Send(Command);
            return RedirectToPage("./Index");
        }
    }
}