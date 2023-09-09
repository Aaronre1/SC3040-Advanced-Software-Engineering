using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.TodoLists.Queries;

public class ToDoListDto
{
    public ToDoListDto()
    {
        Items = Array.Empty<ToDoItemDto>();
    }
    
    public int Id { get; init; }

    public string? Title { get; set; }
    
    public IReadOnlyCollection<ToDoItemDto> Items { get; init; }
    
    private class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<ToDoList, ToDoListDto>();
        }
    }
}