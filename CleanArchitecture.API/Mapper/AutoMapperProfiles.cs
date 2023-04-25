using AutoMapper;
using CleanArchitecture.Application.Dto;
using CleanArchitecture.Domain;

namespace CleanArchitecture.API.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserValuesDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
