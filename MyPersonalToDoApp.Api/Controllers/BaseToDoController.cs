using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Controllers
{    
    public class BaseToDoController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICustomerRepository _customerRepo;

        public BaseToDoController(
            UserManager<ApplicationUser> userManager,
            ICustomerRepository repoCustomer
            )
        {
            this._userManager = userManager;
            this._customerRepo = repoCustomer;
        }

        protected async Task<ApplicationUser> GetUser()
        {
            if (this.User != null)
            {
                return await _userManager.GetUserAsync(this.User);
            }

            return null;
        }

        protected async Task<Customer> GetCustomer()
        {
            ApplicationUser appUser = await this.GetUser();
            Customer customer = this._customerRepo.GetByApplicationUserId(appUser?.Id);

            return customer;
        }
    }
}
