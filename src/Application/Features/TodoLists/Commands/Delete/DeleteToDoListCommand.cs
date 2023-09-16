using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;

namespace ASE3040.Application.Features.TodoLists.Commands.Delete;

public class DeleteToDoListCommand : IRequest<Result>
{
    public int Id { get; set; }
}

public class DeleteToDoListCommandHandler : IRequestHandler<DeleteToDoListCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public DeleteToDoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<Result> Handle(DeleteToDoListCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoLists
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return Result.Failure(new []{ "Entity not found." });
        }

        _context.ToDoLists.Remove(entity);

        var result = await _context.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            return Result.Failure(new []{ "Unable to delete list." });
        }

        return Result.Success();

    }
}