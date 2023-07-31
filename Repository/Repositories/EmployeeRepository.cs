using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

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

    public async Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges)
        => await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(employeeId), trackChanges).SingleOrDefaultAsync();

    public IEnumerable<Employee> GetEmployees(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        => FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToList();

    public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges)
        => await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            .Take(employeeParameters.PageSize)
            .ToListAsync();
}
