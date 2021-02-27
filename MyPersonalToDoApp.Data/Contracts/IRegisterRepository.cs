using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data.Contracts
{
    public interface IRegisterRepository
    {
        Task<bool> RegisterCustomer(ApplicationUser user, Customer customer, string password);
    }
}
