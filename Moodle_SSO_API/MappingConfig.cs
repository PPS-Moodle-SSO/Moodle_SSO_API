using AutoMapper;
using Moodle_SSO_API.Models;
using Moodle_SSO_API.DTO_s;

namespace Moodle_SSO_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<Account, UpdateAccountDTO>().ReverseMap();

        }
    }
}
