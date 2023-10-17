using ASE3040.Application.Features.Itineraries.Queries.Calendar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Calender;

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

    public async Task<IActionResult> OnGetCalendarEventsAsync()
    {
        return new JsonResult(await _sender.Send(new GetItinerariesCalendarEvents()));
    }
}