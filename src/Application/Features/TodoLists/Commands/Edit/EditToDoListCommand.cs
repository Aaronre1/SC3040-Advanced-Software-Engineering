using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Features.TodoLists.Commands.Edit;

public class EditToDoListCommand : IRequest<Result>
{
    public int Id { get; set; }

    public string? Title { get; set; }
}

public class EditToDoListCommandHandler : IRequestHandler<EditToDoListCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public EditToDoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(EditToDoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoLists
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Entity not found." });
        }

        entity.Title = request.Title;

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Result.Failure(new []{ "Unable to update list." });
        }
        
        return Result.Success();
    }   
}