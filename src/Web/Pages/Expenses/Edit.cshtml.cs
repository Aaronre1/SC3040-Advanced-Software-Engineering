using ASE3040.Application.Features.TodoLists.Commands.Edit;
using ASE3040.Application.Features.TodoLists.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Expenses
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
        public EditToDoListCommand Command { get; set; } = new EditToDoListCommand();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var toDoList = await _mediator.Send(new GetToDoList { Id = id });
            if (toDoList == null)
            {
                return NotFound();
            }

            Command.Id = toDoList.Id;
            Command.Title = toDoList.Title;
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