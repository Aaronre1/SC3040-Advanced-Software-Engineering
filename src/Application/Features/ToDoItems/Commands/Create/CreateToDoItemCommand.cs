using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.ToDoItems.Commands.Create;

public class CreateToDoItemCommand : IRequest<Result>
{
    public int ToDoListId { get; set; }
    
    public string? Title { get; set; }
    
    public string? Note { get; set; }
    
    public DateTime DateTime { get; set; }

}

public class CreateToDoItemCommandHandler : IRequestHandler<CreateToDoItemCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(CreateToDoItemCommand request, CancellationToken cancellationToken)
    {
        var list = await _context.ToDoLists
            .Include(x=>x.Items)
            .FirstOrDefaultAsync(x=>x.Id == request.ToDoListId, cancellationToken);

        if (list == null)
        {
            return Result.Failure(new[] { "List is not found." });
        }
        
        list.Items.Add(new ToDoItem()
        {
           Title = request.Title,
           Note = request.Note,
           DateTime = request.DateTime
        });

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Result.Failure(new []{"Unable to add item."});
        }

        return Result.Success();
    }
}
