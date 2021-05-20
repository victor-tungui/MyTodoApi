using MyPersonalToDoApp.Data.Paging;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Contracts
{
    public interface IActivityRepository : IBaseRepository<Activity>
    {
        PagingResult<Activity> GetActivities(ActivityFilter filter);
    }
}
