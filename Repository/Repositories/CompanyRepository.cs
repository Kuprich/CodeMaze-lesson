using Contracts.Repositories;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories;

public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
{
    public CompanyRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    { }


    public void CreateCompany(Company company) =>
        Create(company);

    public void CreateCompanies(IEnumerable<Company> companies) =>
        CreateCollection(companies);

    public IEnumerable<Company> GetAllCompanies(bool trackChanges) =>
        FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToList();
    public async Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges) =>
       await FindAll(trackChanges)
            .OrderBy(c => c.Name)
            .ToListAsync();

    public IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids, bool trackChanges) =>
        FindByCondition(c => ids.Contains(c.Id), trackChanges).ToList();
    public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> ids, bool trackChanges) =>
        await FindByCondition(c => ids.Contains(c.Id), trackChanges).ToListAsync();

    public Company? GetCompany(Guid companyId, bool trackChanges) =>
        FindByCondition(c => c.Id.Equals(companyId), trackChanges).FirstOrDefault();
    public async Task<Company?> GetCompanyAsync(Guid companyId, bool trackChanges) =>
        await FindByCondition(c => c.Id.Equals(companyId), trackChanges).FirstOrDefaultAsync();

    public void DeleteCompany(Company company)
        => Delete(company);

}
