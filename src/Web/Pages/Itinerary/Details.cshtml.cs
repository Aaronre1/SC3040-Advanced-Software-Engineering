using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Application.Features.Itineraries.Commands.Edit;
using ASE3040.Application.Features.Itineraries.Queries;
using ASE3040.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.Differencing;

namespace ASE3040.Web.Pages.Itinerary;

public class Details : PageModel
{
    private readonly ISender _mediator;

    public Details(ISender mediator)
    {
        _mediator = mediator;
    }

    public ItineraryDto Itinerary { get; set; } = default!;

    [BindProperty] public EditItineraryCommand EditItinerary { get; set; }
    [BindProperty] public CreateActivityCommand CreateActivity { get; set; }

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var results = await _mediator.Send(new GetItineraries());
        var itinerary = results.FirstOrDefault(x => x.Id == id);
        if (itinerary == null)
        {
            return NotFound();
        }

        Itinerary = itinerary;
        return Page();
    }

    public async Task<IActionResult> OnPostEdit()
    {
        var result = await _mediator.Send(EditItinerary);

        if (!result.Succeeded)
        {
            ModelState.AddResult(result);
            return await OnGetAsync(EditItinerary.Id);
        }

        return RedirectToPage("Details", new { EditItinerary.Id });
    }

    public async Task<IActionResult> OnPostCreateActivity()
    {
        var result = await _mediator.Send(CreateActivity);

        if (!result.Succeeded)
        {
            ModelState.AddResult(result);
            return await OnGetAsync(CreateActivity.ItineraryId);
        }

        return RedirectToPage("Details", new { Id = CreateActivity.ItineraryId });
    }
}