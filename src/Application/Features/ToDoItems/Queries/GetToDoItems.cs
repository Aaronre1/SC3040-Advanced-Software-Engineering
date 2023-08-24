using System;
using ASE3040.Application.Common.Interfaces;

namespace ASE3040.Application.Features.ToDoItems.Queries
{
	public class GetToDoItemsQuery : IRequest<List<ToDoItemDto>>
	{
	}

	public class GetToDoItemsQueryHandler : IRequestHandler<GetToDoItemsQuery, List<ToDoItemDto>>
	{
		private readonly IApplicationDbContext _context;
		private readonly IMapper _mapper;

		public GetToDoItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public async Task<List<ToDoItemDto>> Handle(GetToDoItemsQuery request, CancellationToken cancellationToken)
        {
			return await _context.ToDoItems
				.ProjectTo<ToDoItemDto>(_mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
        }
    }
}

