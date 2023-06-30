using Contracts;
using Service.Contracts;

namespace Service;

internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _manager;

    public EmployeeService (IRepositoryManager repository, ILoggerManager manager)
    {
        _repository = repository;
        _manager = manager;
    }
}

