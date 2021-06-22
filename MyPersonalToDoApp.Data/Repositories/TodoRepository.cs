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

        public override Todo GetById(long id)
        {
            var todo = this.DbContext.ToDos.Where(t => t.Id == id).Include(t => t.Activity).FirstOrDefault();

            return todo;
        }

        public bool Delete(long id, long customerId)
        {
            var todo = this.DbContext.ToDos.Where(t => t.Id == id && t.Activity.CustomerId == customerId).FirstOrDefault();

            if (todo == null)
            {
                return false;
            }

            base.Delete(todo);

            return true;
        }
    }
}
