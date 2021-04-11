using MyPersonalToDoApp.Data.Contracts;
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

        public IEnumerable<Activity> GetActivities(long customerId, int status, string name)
        {
            if (!Enum.TryParse<DataModel.Status>(status.ToString(), out DataModel.Status eStatus)) {
                eStatus = DataModel.Status.All;
            }

            if (string.IsNullOrEmpty(name))
            {
                name = string.Empty;
            }

            IQueryable<Activity> query = this.DbContext.Activities.Where(a => a.Name.StartsWith(name) && a.CustomerId == customerId);
            if (eStatus != DataModel.Status.All) {
                query = query.Where(a => a.Status == eStatus);
            }

            return query;
        }
    }
}
