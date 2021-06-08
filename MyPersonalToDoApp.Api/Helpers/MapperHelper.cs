using MyPersonalToDoApp.Data.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Helpers
{
    public static class MapperHelper
    {
        public static PagingResult<U> Transform<T, U>(this PagingResult<T> paging, IList<U> items) where T: class where U: class 
        {
            var instance = new PagingResult<U>
            {
                Items = items,
                CurrentPage = paging.CurrentPage,
                PageSize = paging.PageSize,
                TotalPages = paging.TotalPages,
                TotalRows = paging.TotalRows
            };

            return instance;
        }
    }
}
