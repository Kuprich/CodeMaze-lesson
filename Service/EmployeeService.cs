using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;
using Shared.RequestFeatures;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _manager;
    private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repository, ILoggerManager manager, IMapper mapper)
    {
        _repository = repository;
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);

        var employee = _mapper.Map<Employee>(employeeForCreationDto);

        _repository.Employee.CreateEmployee(companyId, employee);
        await _repository.SaveAsync();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);

        var employee = await GetEmployeeForCompanyAndCheckIfItExist(companyId, employeeId, trackChanges);

        _repository.Employee.DeleteEmployee(companyId, employee);
        await _repository.SaveAsync();
    }

    public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);

        var employee = await GetEmployeeForCompanyAndCheckIfItExist(companyId, employeeId, trackChanges);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public async Task<(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync(Guid companyId, Guid employeeId, bool companyTrackChanges, bool employeeTrackChanges)
    {
        await CheckIfCompanyExist(companyId, companyTrackChanges);

        var employee = await GetEmployeeForCompanyAndCheckIfItExist(companyId, employeeId, employeeTrackChanges);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employee);

        return (employeeToPatch, employee);
    }

    public async Task<PagedList<EmployeeDto>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
    {
        await CheckIfCompanyExist(companyId, trackChanges);

        var employeesPagedList = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesPagedList);

        return new PagedList<EmployeeDto>(employeesDto, employeesPagedList.MetaData);

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
        await CheckIfCompanyExist(companyId, companyTrackChanges);

        var employee = await GetEmployeeForCompanyAndCheckIfItExist(companyId, employeeId, employeeTrackChanges);

        _mapper.Map(employeeForUpdateDto, employee);
        await _repository.SaveAsync();
    }

    private async Task CheckIfCompanyExist(Guid companyId, bool trackChanges)
    {
        if (await _repository.Company.GetCompanyAsync(companyId, trackChanges) == null)
            throw new CompanyNotFoundException(companyId);

    }
    private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExist(Guid companyId, Guid employeeId, bool trackChanges) =>
        await _repository.Employee.GetEmployeeAsync(companyId, employeeId, trackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);
}

