using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.Data.Paging;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPersonalToDoApp.Api.Helpers;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/activities")]
    [ApiController]
    [ApiVersion("1")]
    public class ActivityController : BaseToDoController
    {
        private readonly IActivityRepository _activityRepo;
        private readonly IMapper _mapper;
        

        public ActivityController(
            IActivityRepository activityRepo,
            ICustomerRepository customerRepo,
            IMapper mapper, 
            UserManager<ApplicationUser> userManager): base(userManager, customerRepo)
        {
            this._activityRepo = activityRepo;
            this._mapper = mapper;
        }

        [HttpGet()]
        public async Task<ActionResult<IList<ActivityDTO>>> GetActivities([FromQuery] ActivityFilterDTO filter)
        {
            Customer customer = await this.GetCustomer();
            ActivityFilter activityFilter = this._mapper.Map<ActivityFilterDTO, ActivityFilter>(filter);
            activityFilter.CustomerId = customer.Id;

            PagingResult<Activity> queryResult = this._activityRepo.GetActivities(activityFilter);

            IList<ActivityDTO> activityList = this._mapper.Map<IList<Activity>, IList<ActivityDTO>>(queryResult.Items);

            PagingResult<ActivityDTO> model = queryResult.Transform(activityList);

            return Ok(model);
        }

        [HttpGet("{id:long}")]
        public async Task<ActionResult<ActivityDTO>> Get(long id)
        {
            Activity activity = this._activityRepo.GetById(id);
            Customer customer = await this.GetCustomer();
            if (activity == null || activity.CustomerId != customer?.Id)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            ActivityDTO dto = this._mapper.Map<Activity, ActivityDTO>(activity);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityCreatedDTO>> Post([FromBody] ActivityCreationDTO model)
        {
            Customer customer = await this.GetCustomer();
            if (customer == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Activity activity = this._mapper.Map<ActivityCreationDTO, Activity>(model);
            activity.Created = DateTime.UtcNow;
            activity.Status = DataModel.Status.Open;
            activity.Customer = customer;
            if (activity.Expiration.HasValue)
            {
                activity.Expiration = activity.Expiration.Value.ToUniversalTime();
            }            

            this._activityRepo.Add(activity);
            this._activityRepo.Commit();

            return Ok(new ActivityCreatedDTO(activity.Id));
        }

        [HttpPut("{id:long}")]
        public async Task<ActionResult> Put(long id, [FromBody]ActivityCreationDTO model)
        {
            Activity entity = this._activityRepo.GetById(id);
            Customer customer = await this.GetCustomer();
            if (entity == null || entity.CustomerId != customer?.Id)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            entity.Name = model.Name;
            entity.Description = model.Description;            
            if (model.Expiration.HasValue)
            {
                entity.Expiration = model.Expiration.Value.ToUniversalTime();
            }

            this._activityRepo.Update(entity);
            this._activityRepo.Commit();

            return NoContent();
        }

        [HttpDelete("{id:long}")]
        public async Task<ActionResult> Delete(long id)
        {
            Activity entity = this._activityRepo.GetById(id);
            Customer customer = await this.GetCustomer();

            if (entity == null || entity.CustomerId != customer?.Id)
            {
                return BadRequest();
            }

            this._activityRepo.Delete(entity);
            this._activityRepo.Commit();

            return NoContent();
        }
    }
}
