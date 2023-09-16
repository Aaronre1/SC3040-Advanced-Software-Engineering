using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Features.ToDoItems.Commands.Edit;

public class EditToDoItemCommand : IRequest<Result>
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Note { get; set; }

    public bool Done { get; set; }
    
    public DateTime DateTime { get; set; }

}

public class EditToDoItemCommandHandler : IRequestHandler<EditToDoItemCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public EditToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(EditToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure((new[] { "Entity not found." }));
        }

        entity.Title = request.Title;
        entity.Note = request.Note;
        entity.Done = request.Done;
        entity.DateTime = request.DateTime;

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Result.Failure(new[] { "Unable to update item." });
        }

        return Result.Success();

    }
}