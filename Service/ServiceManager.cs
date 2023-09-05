using AutoMapper;
using Contracts;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace Service;

public class ServiceManager : IServiceManager
{
    public ServiceManager(IRepositoryManager repository, IDataShaper<EmployeeDto> dataShaper, ILoggerManager logger, IMapper mapper)
    {
        _companyService = new Lazy<ICompanyService>(new CompanyService(repository, logger, mapper));
        _employeeService = new Lazy<IEmployeeService>(new EmployeeService(repository, dataShaper, logger, mapper));
    }

    private readonly Lazy<ICompanyService> _companyService;
    private readonly Lazy<IEmployeeService> _employeeService;


    public ICompanyService CompanyService => _companyService.Value;
    public IEmployeeService EmployeeService => _employeeService.Value;
}
