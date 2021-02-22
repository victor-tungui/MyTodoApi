using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPersonalToDoApp.Data.Contracts;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Controllers
{
    [Route("api/activities")]
    [ApiController]
    [ApiVersion("1")]    
    public class ActivityController : ControllerBase
    {
        private readonly IActivityRepository _repository;
        private readonly IMapper _mapper;

        public ActivityController(IActivityRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<ActivityDTO>> GetActivities([FromQuery] int status = -1, [FromQuery] string name = "")
        {
            IEnumerable<Activity> queryResult = this._repository.GetActivities(status, name);

            IEnumerable<ActivityDTO> result = this._mapper.Map<IEnumerable<Activity>, IEnumerable<ActivityDTO>>(queryResult);

            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public ActionResult<ActivityDTO> Get(long id)
        {
            Activity activity = this._repository.GetById(id);
            if (activity == null)
            {
                return NoContent();
            }

            ActivityDTO dto = this._mapper.Map<Activity, ActivityDTO>(activity);

            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<ActivityCreatedDTO> Post([FromBody] ActivityCreationDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Activity activity = this._mapper.Map<ActivityCreationDTO, Activity>(model);
            activity.Created = DateTime.UtcNow;
            activity.Status = DataModel.Status.Open;
            if (activity.Expiration.HasValue)
            {
                activity.Expiration = activity.Expiration.Value.ToUniversalTime();
            }

            this._repository.Add(activity);
            this._repository.Commit();

            return Ok(new ActivityCreatedDTO(activity.Id));
        }
    }
}
