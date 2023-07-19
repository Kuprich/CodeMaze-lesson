﻿using Contracts.Repositories;
using Entities.Models;

namespace Repository.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    { }

    public void CreateEmployee(Guid companyId, Employee employee)
    {
        employee.CompanyId = companyId;
        Create(employee);
    }

    public void DeleteEmployee(Guid companyId, Employee employee) 
        => Delete(employee);

    public Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChanges) 
        => FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefault();

    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges) 
        => FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges).ToList();
}
