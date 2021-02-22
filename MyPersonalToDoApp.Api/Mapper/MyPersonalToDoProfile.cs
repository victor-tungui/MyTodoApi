using AutoMapper;
using MyPersonalToDoApp.DataModel.DTOs;
using MyPersonalToDoApp.DataModel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalToDoApp.Api.Mapper
{
    public class MyPersonalToDoProfile : Profile
    {
        public MyPersonalToDoProfile()
        {
            // Entities to DTOs
            CreateMap<Activity, ActivityDTO>();

            // DTOs to Entities
            CreateMap<ActivityCreationDTO, Activity>();
        }
    }
}
