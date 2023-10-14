using ASE3040.Application.Features.Activities.Commands.Create;
using ASE3040.Application.Features.Itineraries.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Itinerary;

public class Details : PageModel
{
    private readonly ISender _mediator;

    public Details(ISender mediator)
    {
        _mediator = mediator;
    }
    [BindProperty]
    public ItineraryDto Itinerary { get; set; } = default!;
    [BindProperty]
    public CreateActivityCommand CreateInput { get; set; }
    
    public async Task OnGetAsync(int id)
    {
        // TODO: Implement get itinerary query
        var results = await _mediator.Send(new GetItineraries());

        Itinerary = results.FirstOrDefault(x => x.Id == id);
    }

    public async Task<IActionResult> OnPostCreate()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Return error model
        CreateInput.ItineraryId = Itinerary.Id;
        var result = await _mediator.Send(CreateInput);
        
        return RedirectToPage($"Details", new {Itinerary.Id});
    }
}