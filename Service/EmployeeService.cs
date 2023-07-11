using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _manager;
    private readonly IMapper _mapper;

    public EmployeeService (IRepositoryManager repository, ILoggerManager manager, IMapper mapper)
    {
        _repository = repository;
        _manager = manager;
        _mapper = mapper;
    }

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        if (_repository.Company.GetCompany(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employees = _repository.Employee.GetEmployees(companyId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return employeesDto;
    }
}

