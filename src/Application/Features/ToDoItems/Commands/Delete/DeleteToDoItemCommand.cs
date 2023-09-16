using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Features.ToDoItems.Commands.Delete;

public class DeleteToDoItemCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteToDoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new[] { "Entity not found." });
        }

        _context.ToDoItems.Remove(entity);
        
        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Result.Failure(new[] { "Unable to delete item." });
        }

        return Result.Success();
    }
}