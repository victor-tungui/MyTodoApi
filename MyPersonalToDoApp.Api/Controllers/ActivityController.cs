using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using MyPersonalToDoApp.DataModel.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Authorize]
    [Route("api/activities")]
    [ApiController]
    [ApiVersion("1")]
    public class ActivityController : BaseToDoController
    {
        private readonly IActivityRepository _activityRepo;
        private readonly ICustomerRepository _customerRepo;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public ActivityController(
            IActivityRepository activityRepo,
            ICustomerRepository customerRepo,
            IMapper mapper, 
            UserManager<ApplicationUser> userManager): base(userManager, customerRepo)
        {
            this._activityRepo = activityRepo;
            this._customerRepo = customerRepo;
            this._mapper = mapper;
            this.userManager = userManager;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivities([FromQuery] int status = -1, [FromQuery] string name = "")
        {
            Customer customer = await this.GetCustomer();
            IEnumerable<Activity> queryResult = this._activityRepo.GetActivities(customer.Id, status, name);

            IEnumerable<ActivityDTO> result = this._mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityDTO>>(queryResult);

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<ActivityDTO> Get(long id)
        {
            Activity activity = this._activityRepo.GetById(id);
            if (activity == null)
            {
                return BadRequest();
            }

            ActivityDTO dto = this._mapper.Map<Activity, ActivityDTO>(activity);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<ActivityCreatedDTO>> Post([FromBody] ActivityCreationDTO model)
        {
            ApplicationUser appUser = await base.GetUser();
            if (appUser == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            Customer customer = this._customerRepo.GetByApplicationUserId(appUser.Id);

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
        public ActionResult Put(long id, [FromBody]ActivityCreationDTO model)
        {
            Activity entity = this._activityRepo.GetById(id);
            if (entity == null)
            {
                return BadRequest();
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
        public ActionResult Delete(long id)
        {
            Activity entity = this._activityRepo.GetById(id);
            if (entity == null)
            {
                return BadRequest();
            }

            this._activityRepo.Delete(entity);
            this._activityRepo.Commit();

            return NoContent();
        }
    }
}
