using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.Entities;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

namespace YoungpotentialsAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AspNetUsers, AuthenticationResponse>();
            CreateMap<UserRegistrationRequest, AspNetUsers>();
            CreateMap<UserUpdateRequest, AspNetUsers>();
            CreateMap<UserRegistrationRequest, Students>();
            CreateMap<UserRegistrationRequest, Companies>();
            CreateMap<Studiegebied, StudiegebiedResponse>();
            CreateMap<StudiegebiedRequest, Studiegebied>();
            CreateMap<Opleiding, OpleidingResponse>();
            CreateMap<OpleidingRequest, Opleiding>();
        }
    }
}
