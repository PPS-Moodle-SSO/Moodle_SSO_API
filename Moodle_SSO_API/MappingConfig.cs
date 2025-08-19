using AutoMapper;
using Moodle_SSO_API.Services.Moodle.Models;
using Moodle_SSO_API.Handlers.Moodles.Models;
using Moodle_SSO_API.Controllers.Moodle.Responses;
using Moodle_SSO_API.Controllers.Enterprises.Requests;
using Moodle_SSO_API.Handlers.Enterprises.ModelsDto;
using Moodle_SSO_API.Controllers.Enterprises.Responses;
using Moodle_SSO_API.Models;

namespace Moodle_SSO_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            //Enterprise Mapping
            CreateMap<UpdateEnterpriseRequest, UpdateEnterpriseRequestDto>().ReverseMap();
            CreateMap<Enterprise, UpdateEnterpriseResponse>().ReverseMap();

            //Moodle Mapping
            CreateMap<GetUserResponseDto, GetUserResponse>().ReverseMap();
            CreateMap<Services.Moodle.Models.GetUserByEmailResponse, GetUserResponseDto>().ReverseMap();
            CreateMap<Services.Moodle.Models.AuthenticateResponse, AuthenticateResponseDto>().ReverseMap();
            CreateMap<AuthenticateResponseDto, Controllers.Moodle.Responses.AuthenticateResponse>()
                .ForMember(dest => dest.UserData, opt => opt.MapFrom((src, dest, destMember, context) =>
                    src.UserData != null ? src.UserData.Select(x => context.Mapper.Map<GetUserResponse>(x)).ToList() : null))
                .ReverseMap();
        }
    }
}
