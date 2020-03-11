using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Youngpotentials.Domain.Entities;
using Youngpotentials.Domain.Models.Requests;
using Youngpotentials.Domain.Models.Responses;
using YoungpotentialsAPI.Models.Requests;
using YoungpotentialsAPI.Models.Responses;

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
            CreateMap<Studiegebied, StudiegebiedResponse>();
            CreateMap<Opleiding, OpleidingResponse>();
            CreateMap<Afstudeerrichting, AfstudeerrichtingResponse>();
            CreateMap<Keuze, KeuzeResponse>();
            CreateMap<Studiegebied, StudiegebiedResponseDetail>();
            CreateMap<Opleiding, OpleidingResponseDetail>();
            CreateMap<Afstudeerrichting, AfstudeerrichtingResponseDetail>();
            CreateMap<AspNetUsers, UserResponse>();
            CreateMap<Students, StudentResponse>();
            CreateMap<Companies, CompanyResponse>();
            CreateMap<Youngpotentials.Domain.Entities.Type, TypeResponse>();
            CreateMap<Object, TypeResponse>();

            //Requests
            CreateMap<UserRegistrationRequest, AspNetUsers>();
            CreateMap<UserUpdateRequest, AspNetUsers>();
            CreateMap<UserRegistrationRequest, Students>();
            CreateMap<UserRegistrationRequest, Companies>();
            CreateMap<StudiegebiedRequest, Studiegebied>();
            CreateMap<OpleidingRequest, Opleiding>();            
            CreateMap<AfstudeerrichtingRequest, Afstudeerrichting>();
            CreateMap<KeuzeRequest, Keuze>();
            CreateMap<CreateOfferRequest, Offers>();
            CreateMap<UpdateOfferRequest, Offers>();
            
        }
    }
}
