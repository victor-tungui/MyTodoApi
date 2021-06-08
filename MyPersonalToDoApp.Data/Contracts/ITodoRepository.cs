using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Contracts
{
    public interface ITodoRepository : IBaseRepository<Todo>
    {
        //Task<IEnumerable<Todo>> GetTodos(long activityId, long customerId);

        IEnumerable<Todo> GetTodos(long activityId, long customerId);
    }
}
