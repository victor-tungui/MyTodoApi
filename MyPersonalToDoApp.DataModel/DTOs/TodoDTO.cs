using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.DataModel.DTOs
{
    public record TodoItemDTO
    {
        public long Id { get; set; }

        public string Name { get; set; } 

        /// <summary>
        /// 1 - Open
        /// 3 Closed
        /// </summary>
        public int Status { get; set; }
    }

    public record TodoDTO
    {
        public TodoDTO()
        {
            this.Items = new List<TodoItemDTO>();
        }

        public int Total { get; set; }

        public int Open { get; set; }

        public int Closed { get; set; }

        public IEnumerable<TodoItemDTO> Items { get; set; }
    }
}
