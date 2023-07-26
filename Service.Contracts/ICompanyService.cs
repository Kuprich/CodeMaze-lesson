using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
    Task<IEnumerable<CompanyDto>> GetCompaniesAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
    Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto companyForCreationDto);
    Task<IEnumerable<CompanyDto>> CreateCompaniesAsync(IEnumerable<CompanyForCreationDto> companiesForCreationDto);
    Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
    Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges);
}
