using Microsoft.AspNetCore.Identity;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.Data.Repositories;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Data
{
    public class RegisterRepository : BaseRepository<Customer>, IRegisterRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterRepository(ToDoContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            this._userManager = userManager;
        }

        public async Task<bool> RegisterCustomer(ApplicationUser user, Customer customer, string password)
        {
            var existingUser = await _userManager.FindByEmailAsync(user.Email);
            if (existingUser != null)
            {
                return false;
            }            

            // Create user
            IdentityResult creationResult = await _userManager.CreateAsync(user, password);

            if (!creationResult.Succeeded)
            {
                return false;
            }

            // Create customer
            customer.UserId = user.Id;
            this.Add(customer);
            this.Commit();

            return true;
        }
    }
}
