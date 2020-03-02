using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Domain.Models.Requests;
using Youngpotentials.Domain.Models.Responses;

namespace YoungpotentialsAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //Responses
            CreateMap<AspNetUsers, AuthenticationResponse>();
            CreateMap<Studiegebied, StudiegebiedResponse>();
            CreateMap<Offers, OfferResponse>();

            //Requests
            CreateMap<UserRegistrationRequest, AspNetUsers>();
            CreateMap<UserUpdateRequest, AspNetUsers>();
            CreateMap<UserRegistrationRequest, Students>();
            CreateMap<UserRegistrationRequest, Companies>();
            CreateMap<CreateOfferRequest, Offers>();
            CreateMap<UpdateOfferRequest, Offers>();

        }
    }
}
