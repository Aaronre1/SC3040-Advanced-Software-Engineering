using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.TodoLists.Queries;

[Authorize]
public class GetToDoLists: IRequest<List<ToDoListDto>>
{
}

public class GetToDoListsHandler : IRequestHandler<GetToDoLists, List<ToDoListDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;
    
    public GetToDoListsHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }
    
    public async Task<List<ToDoListDto>> Handle(GetToDoLists request, CancellationToken cancellationToken)
    {
        var result = await _context.ToDoLists.AsNoTracking()
            .Include(i => i.Items)
            .Where(i => i.CreatedBy == _user.UserId)
            .ProjectTo<ToDoListDto>(_mapper.ConfigurationProvider)
            .OrderBy(i => i.Title)
            .ToListAsync(cancellationToken);
        
        return result;
    }
}