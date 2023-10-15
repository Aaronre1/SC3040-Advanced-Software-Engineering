using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Expenses;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ISender _mediator;

    public IndexModel(ISender mediator)
    {
        _mediator = mediator;
    }

    public void OnGet()
    {
    }
}