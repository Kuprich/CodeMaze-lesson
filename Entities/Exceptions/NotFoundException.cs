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