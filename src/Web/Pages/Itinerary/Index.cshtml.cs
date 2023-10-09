using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASE3040.Application.Features.Itineraries.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASE3040.Web.Pages.Itinerary;

[Authorize]
public class IndexModel : PageModel
{
    private readonly ISender _mediator;

    public IndexModel(ISender mediator)
    {
        _mediator = mediator;
    }
    
    public IEnumerable<ItineraryDto> Itineraries { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Itineraries = await _mediator.Send(new GetItineraries());

        return Page();
    }
}