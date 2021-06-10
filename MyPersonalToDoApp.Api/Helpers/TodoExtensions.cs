using MyPersonalToDoApp.DataModel;
using MyPersonalToDoApp.DataModel.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Helpers
{
    public static class TodoExtensions
    {
        public static string ToGuidString(this Guid guid)
        {
            return guid.ToString().ToLower();
        }

        public static TodoDTO ToTodoDTO(this IList<TodoItemDTO> items)
        {
            TodoDTO dto = new TodoDTO
            {
                Items = items,
                Total = items.Count,
                Closed = items.Where(i => i.Status == (int)Status.Closed).Count(),
                Open = items.Where(i => i.Status == (int)Status.Open).Count()
            };

            return dto;
        }
    }
}
