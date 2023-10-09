using ASE3040.Application.Common.Interfaces;
using ASE3040.Application.Common.Security;

namespace ASE3040.Application.Features.Itineraries.Queries;

[Authorize]
public class GetItineraries : IRequest<IEnumerable<ItineraryDto>>
{
}

public class GetItinerariesQueryHandler : IRequestHandler<GetItineraries, IEnumerable<ItineraryDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public GetItinerariesQueryHandler(IApplicationDbContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    public async Task<IEnumerable<ItineraryDto>> Handle(GetItineraries request, CancellationToken cancellationToken)
    {
        var result = await _context.Itineraries.AsNoTracking()
            .Include(i => i.Activities)
            .Where(i => i.CreatedBy == _user.UserId)
            .ProjectTo<ItineraryDto>(_mapper.ConfigurationProvider)
            .OrderBy(i => i.LastModified)
            .ToListAsync(cancellationToken);
        return result;
    }
}