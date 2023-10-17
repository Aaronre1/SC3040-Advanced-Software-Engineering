using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.AttractionView;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ISender _sender;

    public IndexModel(ISender sender)
    {
        _sender = sender;
    }

    public void OnGet()
    {
    }
    // TODO: Add activity with dropdown selection of itineraries or create new itinerary
}