using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.testEntities;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AspNetUser, UserResponse>();
            CreateMap<UserRegistrationRequest, AspNetUser>();
            CreateMap<UserUpdateRequest, AspNetUser>();
        }
    }
}
