﻿using Shared.DataTransferObjects.Company;

namespace Service.Contracts;

public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
    IEnumerable<CompanyDto> GetCompanies(IEnumerable<Guid> ids, bool trackChanges);
    CompanyDto GetCompany(Guid companyId, bool trackChanges);
    CompanyDto CreateCompany(CompanyForCreationDto companyForCreationDto);
    IEnumerable<CompanyDto> CreateCompanies(IEnumerable<CompanyForCreationDto> companiesForCreationDto);
    void DeleteCompany(Guid companyId, bool trackChanges);
}
