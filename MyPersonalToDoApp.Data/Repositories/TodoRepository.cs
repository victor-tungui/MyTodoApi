using Microsoft.EntityFrameworkCore;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Repositories
{
    public class TodoRepository : BaseRepository<Todo>, ITodoRepository
    {
        public TodoRepository(ToDoContext context) : base(context)
        { 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        //public async Task<IEnumerable<Todo>> GetTodos(long activityId, long customerId)
        public IEnumerable<Todo> GetTodos(long activityId, long customerId)
        {
            var result = this.DbContext.ToDos.Where(t => t.ActivityId == activityId && t.Activity.CustomerId == customerId);

            return result;
        }
    }
}
