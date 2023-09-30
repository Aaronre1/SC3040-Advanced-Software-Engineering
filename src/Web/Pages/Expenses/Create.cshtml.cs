using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASE3040.Application.Features.TodoLists.Commands.Create;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ASE3040.Domain.Entities;
using ASE3040.Infrastructure.Data;
using MediatR;

namespace ASE3040.Web.Pages.Todo
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