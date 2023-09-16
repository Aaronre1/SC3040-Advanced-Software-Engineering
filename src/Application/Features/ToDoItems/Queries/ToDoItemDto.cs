using System;
using ASE3040.Domain.Entities;

namespace ASE3040.Application.Features.ToDoItems.Queries
{
	public class ToDoItemDto
	{
        public int Id { get; set; }

        public int ListId { get; set; }

        public string? Title { get; set; }

        public string? Note { get; set; }

        public bool Done { get; set; }
        
        public DateTime DateTime { get; set; }

        private class Mapping: Profile
        {
            public Mapping()
            {
                CreateMap<ToDoItem, ToDoItemDto>();
            }
        }
        
    }
}

