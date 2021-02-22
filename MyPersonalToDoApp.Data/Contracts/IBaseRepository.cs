using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Contracts
{
    public interface IBaseRepository<T>
    {
        T GetById(long Id);
        IEnumerable<T> Filter(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
        int Commit();
    }
}
