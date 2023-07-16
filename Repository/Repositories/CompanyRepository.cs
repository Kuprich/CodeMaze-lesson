using Contracts.Repositories;
using Entities.Models;

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

    public IEnumerable<Company> GetCompanies(IEnumerable<Guid> ids, bool trackChanges) => 
        FindByCondition(c => ids.Contains(c.Id), trackChanges).ToList();

    public Company? GetCompany(Guid companyId, bool trackChanges) => 
        FindByCondition(c => c.Id.Equals(companyId), trackChanges).FirstOrDefault();
}
