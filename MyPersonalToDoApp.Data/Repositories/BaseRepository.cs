using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Repositories
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class, IEntityBase, new()
    {
        protected ToDoContext DbContext
        {
            get; private set;
        }

        public BaseRepository(ToDoContext context)
        {
            this.DbContext = context;
        }

        public virtual T GetById(long id)
        {
            return this.DbContext.Set<T>().FirstOrDefault(e => e.Id == id);
        }
        
        public virtual IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return this.DbContext.Set<T>().Where(predicate);
        }

        public virtual void Add(T entity)
        {
            this.SetEntityEntry(entity);
            this.DbContext.Set<T>().Add(entity);
        }

        public virtual void Delete(T entity)
        {
            EntityEntry entry = this.SetEntityEntry(entity);
            entry.State = EntityState.Deleted;
        }

        public virtual void Update(T entity)
        {            
            EntityEntry entry = this.SetEntityEntry(entity);
            entry.State = EntityState.Modified;
        }

        private EntityEntry SetEntityEntry(T entity)
        {
            entity.LastUpdate = DateTime.UtcNow;
            return this.DbContext.Entry<T>(entity);
        }

        public virtual int Commit()
        {
            return this.DbContext.SaveChanges();
        }
    }
}
