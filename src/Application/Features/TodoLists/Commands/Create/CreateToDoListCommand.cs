using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Models;
using ASE3040.Application.Common.Security;
using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.TodoLists.Commands.Create;

[Authorize]
public class CreateToDoListCommand : IRequest<Result>
{
    public string? Title { get; set; }
}

public class CreateToDoListCommandHandler : IRequestHandler<CreateToDoListCommand, Result>
{
    private readonly IApplicationDbContext _context;

    public CreateToDoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(CreateToDoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDoList();
        entity.Title = request.Title;

        _context.ToDoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}