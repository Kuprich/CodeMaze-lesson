using Contracts;
using Contracts.Repositories;
using Repository.Repositories;

namespace Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    public RepositoryManager(RepositoryContext repositoryContext)
    {
        _company = new Lazy<ICompanyRepository>(() => new CompanyRepository(repositoryContext));
        _employee = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(repositoryContext));
        _repositoryContext = repositoryContext;
    }

    private readonly Lazy<ICompanyRepository> _company;
    private readonly Lazy<IEmployeeRepository> _employee;
    private readonly RepositoryContext _repositoryContext;

    public ICompanyRepository Company => _company.Value;

    public IEmployeeRepository Employee => _employee.Value;

    public void Save() => _repositoryContext.SaveChanges();
}
