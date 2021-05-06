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
            CreateMap<Activity, ActivityDTO>()
             .ForMember(dto => dto.Created, opts => opts.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToString("o")))
             .ForMember(dto => dto.LastUpdate, opts => opts.MapFrom(src => DateTime.SpecifyKind(src.LastUpdate, DateTimeKind.Utc) .ToString("o")))
             .ForMember(dto => dto.Expiration, opts => opts.MapFrom(src => src.Expiration.HasValue ? DateTime.SpecifyKind(src.Expiration.Value, DateTimeKind.Utc).ToString("o") : null));

            // DTOs to Entities
            CreateMap<ActivityCreationDTO, Activity>();
            CreateMap<ActivityFilterDTO, ActivityFilter>();
        }
    }
}
