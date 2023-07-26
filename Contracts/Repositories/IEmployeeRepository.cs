using Entities.Models;

namespace Contracts.Repositories;

public interface IEmployeeRepository
{
    IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
    Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);
    Employee? GetEmployee(Guid companyId, Guid employeeId, bool trackChanges);
    Task<Employee?> GetEmployeeAsync(Guid companyId, Guid employeeId, bool trackChanges);
    void CreateEmployee(Guid companyId, Employee employee);
    void DeleteEmployee(Guid companyId, Employee employee);
}