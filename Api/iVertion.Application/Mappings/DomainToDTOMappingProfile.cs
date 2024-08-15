using AutoMapper;
using iVertion.Application.DTOs;
using iVertion.Domain.Entities;

namespace iVertion.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            // User Profiles and Roles
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
            CreateMap<RoleProfile, RoleProfileDTO>().ReverseMap();
            CreateMap<AdditionalUserRole, AdditionalUserRoleDTO>().ReverseMap();
            CreateMap<TemporaryUserRole, TemporaryUserRoleDTO>().ReverseMap();

            // Person
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<PersonAddress, PersonAddressDTO>().ReverseMap();

            // Address
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<State, StateDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();

        }
    }
}