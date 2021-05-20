using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Paging
{
    public static class PagingResultExtension
    {
        public static PagingResult<T> Paginate<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var model = new PagingResult<T>();

            if (page <= 0)
            {
                page = 1;
            }

            if (pageSize <= 0 || page > 1000)
            {
                pageSize = 10;
            }

            model.PageSize = pageSize;
            model.CurrentPage = page;

            model.TotalRows = query.Count();

            var pageCount = (double)model.TotalRows / model.PageSize;
            model.TotalPages = (int)Math.Ceiling(pageCount);

            if (page > model.TotalPages)
            {
                model.CurrentPage = 1;
            }

            var skip = (model.CurrentPage - 1) * model.PageSize;
            model.Items = query.Skip(skip).Take(model.PageSize).ToList();

            return model;
        }
    }
}
