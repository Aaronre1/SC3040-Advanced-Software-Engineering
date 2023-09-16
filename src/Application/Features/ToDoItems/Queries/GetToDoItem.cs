using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Application.Features.ToDoItems.Queries;

public class GetToDoItem : IRequest<ToDoItemDto?>
{
    public int Id { get; set; }
}

public class GetToDoItemHandler : IRequestHandler<GetToDoItem, ToDoItemDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetToDoItemHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ToDoItemDto?> Handle(GetToDoItem request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoItems.AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}