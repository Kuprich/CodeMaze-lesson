using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

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

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = _mapper.Map<Employee>(employeeForCreationDto);

        _repository.Employee.CreateEmployee(companyId, employee);
        await _repository.SaveAsync();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, trackChanges) == null)
            throw new CompanyNotFoundException(companyId);

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges) 
            ?? throw new EmployeeNotFoundException(employeeId);

        _repository.Employee.DeleteEmployee(companyId, employee);
        await _repository.SaveAsync();
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid employeeId, bool companyTrackChanges, bool employeeTrackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, companyTrackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, employeeTrackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

        return (employeeToPatch, employeeEntity);
    }

    public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employees = await _repository.Employee.GetEmployeesAsync(companyId, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

        return employeesDto;
    }

    public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateEmployeeForCompanyAsync(Guid companyId,
                                         Guid employeeId,
                                         EmployeeForUpdateDto employeeForUpdateDto,
                                         bool companyTrackChanges,
                                         bool employeeTrackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, companyTrackChanges) == null)
            throw new CompanyNotFoundException(companyId);
        
        var employee = await _repository.Employee.GetEmployeeAsync(companyId, employeeId, employeeTrackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        if (employeeForUpdateDto == null)
            throw new EmployeeBadRequestException();

        _mapper.Map(employeeForUpdateDto, employee);
        await _repository.SaveAsync();
    }
}

