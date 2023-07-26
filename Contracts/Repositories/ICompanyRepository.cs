using Entities.Models;

namespace Contracts.Repositories;

public interface ICompanyRepository
{
    IEnumerable<Company> GetAllCompanies(bool trackChanges);
    Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges);
    IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids,  bool trackChanges);
    Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> ids, bool trackChanges);
    Company? GetCompany(Guid companyId, bool trackChanges);
    Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges);
    void CreateCompany(Company company);
    void CreateCompanies(IEnumerable<Company> companies);
    void DeleteCompany(Company company);

}
