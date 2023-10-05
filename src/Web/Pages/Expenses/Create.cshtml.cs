using ASE3040.Application.Features.TodoLists.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Expenses
{
    public class CreateModel : PageModel
    {
        private readonly ISender _mediator;

        public CreateModel(ISender mediator)
        {
            _mediator = mediator;
        }

        [BindProperty] public string? Title { get; set; } = default!;

        public void OnGet()
        {
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