namespace Entities.Exceptions;

public abstract class NotFoundException : Exception
{
    protected NotFoundException(string? message) 
        : base(message)
    { }

}

public sealed class CompanyNotFoundException : NotFoundException
{
    public CompanyNotFoundException(Guid companyId) 
        : base ($"The company with id {companyId} doesn't exist in the current database.")
    { }
}

public sealed class EmployeeNotFoundException : NotFoundException
{
    public EmployeeNotFoundException(Guid employeeId) 
        : base($"The employee with id {employeeId} doesn't exist in the current database.")
    { }
}