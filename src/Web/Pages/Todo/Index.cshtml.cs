using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASE3040.Application.Features.TodoLists.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASE3040.Domain.Entities;
using ASE3040.Infrastructure.Data;
using MediatR;

namespace ASE3040.Web.Pages.Todo
{
    public class IndexModel : PageModel
    {
        private readonly ISender _mediator;

        public IndexModel(ISender mediator)
        {
            _mediator = mediator;
        }
        public IList<ToDoListDto> ToDoLists { get;set; } = default!;

        public async Task OnGetAsync()
        {
            ToDoLists = await _mediator.Send(new GetToDoLists());
        }
    }
}
