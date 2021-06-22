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
        IEnumerable<Todo> GetTodos(long activityId, long customerId);

        bool Delete(long id, long customerId);
    }
}
