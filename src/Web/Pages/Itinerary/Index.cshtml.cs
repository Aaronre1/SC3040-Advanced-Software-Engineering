using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASE3040.Application.Features.Itineraries.Commands.Create;
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
    
    [BindProperty]
    public CreateModel CreateInput { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Itineraries = await _mediator.Send(new GetItineraries());

        return Page();
    }

    public async Task<IActionResult> OnPostCreateAsync()
    {
        if (!ModelState.IsValid)
        {
            Itineraries = await _mediator.Send(new GetItineraries());
            return Page();
        }
        
        var request = new CreateItineraryCommand()
        {
            Title = CreateInput.Title,
            Description = CreateInput.Description,
            Budget = CreateInput.Budget
        };

        var result = await _mediator.Send(request);
        
        return RedirectToPage();
    }

    public class CreateModel
    {
        [Required]
        public string? Title { get; set; }
        public string? Description { get; set; }
        [DataType(DataType.Currency)]
        public decimal? Budget { get; set; }
    }
}