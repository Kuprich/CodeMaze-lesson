using Entities.Models;

namespace Contracts.Repositories;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids,  bool trackChanges);
    Company? GetCompany(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    void CreateCompanies(IEnumerable<Company> companies);
    void DeleteCompany(Company company);

}
