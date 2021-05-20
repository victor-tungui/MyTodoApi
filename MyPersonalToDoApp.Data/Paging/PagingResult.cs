using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Paging
{
    public record PagingResult<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IList<T> Items { get; set; }
        public int TotalRows { get; set; }

        public PagingResult()
        {
            this.Items = new List<T>();
        }
    }
}
