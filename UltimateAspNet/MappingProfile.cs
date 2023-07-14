using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.Company;

namespace UltimateAspNet;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForMember(c => c.FullAddress,
                       opt => opt.MapFrom(c => string.Concat(c.Country, " ", c.Address)));

        CreateMap<CompanyForCreationDto, Company>();

        CreateMap<Employee, EmployeeDto>();
    }
}
