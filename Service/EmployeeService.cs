﻿using AutoMapper;
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

    public EmployeeDto CreateEmployeeForCompany(Guid companyId, EmployeeForCreationDto employeeForCreationDto, bool trackChanges)
    {
        if (_repository.Company.GetCompany(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = _mapper.Map<Employee>(employeeForCreationDto);

        _repository.Employee.CreateEmployee(companyId, employee);
        _repository.Save();

        return _mapper.Map<EmployeeDto>(employee);
    }

    public void DeleteEmployeeForCompany(Guid companyId, Guid employeeId, bool trackChanges)
    {
        if (_repository.Company.GetCompany(companyId, trackChanges) == null)
            throw new CompanyNotFoundException(companyId);

        var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges) 
            ?? throw new EmployeeNotFoundException(employeeId);

        _repository.Employee.DeleteEmployee(companyId, employee);
        _repository.Save();
    }

    public EmployeeDto GetEmployee(Guid companyId, Guid employeeId, bool trackChanges)
    {
        if (_repository.Company.GetCompany(companyId, trackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employee = _repository.Employee.GetEmployee(companyId, employeeId, trackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        var employeeDto = _mapper.Map<EmployeeDto>(employee);

        return employeeDto;
    }

    public (EmployeeForUpdateDto employeeToPatch, Employee employeeEntity) GetEmployeeForPatch(Guid companyId, Guid employeeId, bool companyTrackChanges, bool employeeTrackChanges)
    {
        if (_repository.Company.GetCompany(companyId, companyTrackChanges) == null)
        {
            throw new CompanyNotFoundException(companyId);
        }

        var employeeEntity = _repository.Employee.GetEmployee(companyId, employeeId, employeeTrackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);

        return (employeeToPatch, employeeEntity);
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

    public void SaveChangesForPatch(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
    {
        _mapper.Map(employeeToPatch, employeeEntity);
        _repository.Save();
    }

    public void UpdateEmployeeForCompany(Guid companyId,
                                         Guid employeeId,
                                         EmployeeForUpdateDto employeeForUpdateDto,
                                         bool companyTrackChanges,
                                         bool employeeTrackChanges)
    {
        if (_repository.Company.GetCompany(companyId, companyTrackChanges) == null)
            throw new CompanyNotFoundException(companyId);
        
        var employee = _repository.Employee.GetEmployee(companyId, employeeId, employeeTrackChanges)
            ?? throw new EmployeeNotFoundException(employeeId);

        if (employeeForUpdateDto == null)
            throw new EmployeeBadRequestException();

        _mapper.Map(employeeForUpdateDto, employee);
        _repository.Save();
    }
}

