using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Todo
{
    public class DetailsModel : PageModel
    {
        private readonly ISender _mediator;

        public DetailsModel(ISender mediator)
        {
            _mediator = mediator;
        }
    /*
        public ToDoListDto? ToDoList { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            ToDoList = await _mediator.Send(new GetToDoList { Id = id });

            if (ToDoList == null)
            {
                return NotFound();
            }
        
            return Page();
        }*/
    } 
}