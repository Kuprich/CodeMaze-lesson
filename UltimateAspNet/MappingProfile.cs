using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;

namespace UltimateAspNet;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Company, CompanyDto>()
            .ForCtorParam(nameof(CompanyDto.FullAddress),
                       opt => opt.MapFrom(x => string.Concat(x.Country, " ", x.Address)));

        CreateMap<Employee, EmployeeDto>();
    }
}
