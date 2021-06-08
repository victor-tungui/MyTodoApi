using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    [Route("api/todos")]
    [ApiController]
    [ApiVersion("1")]
    public class TodosController : BaseToDoController
    {   
        private readonly ITodoRepository _todoRepo;        

        public TodosController(
            ITodoRepository todoRepo,            
            ICustomerRepository customerRepo, 
            UserManager<ApplicationUser> userManager): base(userManager, customerRepo)
        {
            this._todoRepo = todoRepo;            
        }
        
        [HttpGet]
        public async Task<ActionResult<IList<Todo>>> GetTodos([FromQuery] long activityId)
        {
            var customer = await this .GetCustomer();

            var list = this._todoRepo.GetTodos(activityId, customer.Id);

            return Ok(list);
        }
    }
}
