using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Application.Features.TodoLists.Queries;

public class GetToDoList : IRequest<ToDoListDto?>
{
    public int Id { get; set; }
}

public class GetToDoListHandler : IRequestHandler<GetToDoList, ToDoListDto?>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;

    public GetToDoListHandler(IMapper mapper, IApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    
    public async Task<ToDoListDto?> Handle(GetToDoList request, CancellationToken cancellationToken)
    {
        var entity = await _context.ToDoLists.AsNoTracking()
            .Where(x => x.Id == request.Id)
            .ProjectTo<ToDoListDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return entity;
    }
}