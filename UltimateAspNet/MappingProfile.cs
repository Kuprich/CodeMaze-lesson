using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace UltimateAspNet;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
                       opt => opt.MapFrom(c => string.Concat(c.Country, " ", c.Address)));

        CreateMap<Employee, EmployeeDto>();
    }
}
