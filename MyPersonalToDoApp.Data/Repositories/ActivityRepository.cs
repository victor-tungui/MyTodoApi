using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.Data.Paging;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Repositories
{
    public class ActivityRepository : BaseRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(ToDoContext context) : base(context)
        {
        }

        public PagingResult<Activity> GetActivities(ActivityFilter filter)
        {
            if (!Enum.TryParse<DataModel.Status>(filter.Status.ToString(), out DataModel.Status eStatus)) {
                eStatus = DataModel.Status.All;
            }

            if (string.IsNullOrEmpty(filter.Name))
            {
                filter.Name = string.Empty;
            }

            IQueryable<Activity> query = this.DbContext.Activities.Where(a => a.Name.Contains(filter.Name) && a.CustomerId == filter.CustomerId);
            if (eStatus != DataModel.Status.All) {
                query = query.Where(a => a.Status == eStatus);
            }

            PagingResult<Activity> activityPagingResult = query.Paginate<Activity>(filter.Page, filter.PageSize);

            return activityPagingResult;
        }
    }
}
