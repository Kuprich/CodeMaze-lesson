using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployees(Guid companyId, EmployeeParameters employeeParameters,  bool trackChanges);
    Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters employeeParameters, bool trackChanges);
    Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
    Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
    void CreateEmployee(Guid companyId, Employee employee);
    void DeleteEmployee(Guid companyId, Employee employee);
}