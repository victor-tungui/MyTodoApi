using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Repositories
{
    public class CustomerRepository: BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ToDoContext context): base(context)
        {
        }

        public Customer GetByApplicationUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            return this.DbContext.Customers.FirstOrDefault(customer => customer.UserId == userId);
        }
    }
}
