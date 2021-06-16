using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPersonalToDoApp.Api.Helpers;
using Microsoft.AspNetCore.Http;
using System;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/todos")]
    [ApiController]
    [ApiVersion("1")]
    public class TodosController : BaseToDoController
    {   
        private readonly ITodoRepository _todoRepo;
        private readonly IActivityRepository _activityRepo;
        private readonly IMapper _mapper;

        public TodosController(
            ITodoRepository todoRepo,
            IActivityRepository activityRepo,
            ICustomerRepository customerRepo, 
            IMapper mapper,
            UserManager<ApplicationUser> userManager): base(userManager, customerRepo)
        {
            this._todoRepo = todoRepo;
            this._activityRepo = activityRepo;
            this._mapper = mapper;
        }
        
        [HttpGet]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<IList<TodoDTO>>> GetTodos([FromQuery] long activityId)
        {
            var customer = await this .GetCustomer();

            var entities = this._todoRepo.GetTodos(activityId, customer.Id);

            IList<TodoItemDTO> todosDto = this._mapper.Map<IEnumerable<Todo>, IList<TodoItemDTO>>(entities);

            var dto = todosDto.ToTodoDTO();

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<EntityCreatedDTO>> Post([FromBody] TodoCreationDTO model)
        {
            var customer = await this.GetCustomer();
            var activity = this._activityRepo.GetById(model.ActivityId);

            if (activity == null || activity.CustomerId != customer.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var todo = this._mapper.Map<TodoCreationDTO, Todo>(model);
            todo.Status = DataModel.Status.Open;
            todo.Activity = activity;

            this._todoRepo.Add(todo);
            this._todoRepo.Commit();

            return Ok(new EntityCreatedDTO(todo.Id, todo.LastUpdate));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, [FromBody] TodoUpdateDTO model)
        {
            var customer = await this.GetCustomer();

            var todo = this._todoRepo.GetById(id);    
            
            if (todo == null || todo.Activity?.CustomerId != customer.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            todo.Name = model.Name;
            todo.Description = model.Description;
            todo.Status = DataModel.Status.Open;
            if (Enum.TryParse<DataModel.Status>(model.Status.ToString(), out DataModel.Status status))
            {
                todo.Status = status;
            }

            this._todoRepo.Update(todo);
            this._todoRepo.Commit();

            return Ok(new EntityCreatedDTO(todo.Id, todo.LastUpdate));
        }
    }
}
