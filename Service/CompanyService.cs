using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace Service;

internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto companyForCreationDto)
    {
        var company = _mapper.Map<Company>(companyForCreationDto);

        _repository.Company.CreateCompany(company);
        await _repository.SaveAsync();

        return _mapper.Map<CompanyDto>(company);
    }

    public async Task<IEnumerable<CompanyDto>> CreateCompaniesAsync(IEnumerable<CompanyForCreationDto> companiesForCreationDto)
    {
        var companies = _mapper.Map<IEnumerable<Company>>(companiesForCreationDto);
        _repository.Company.CreateCompanies(companies);
        await _repository.SaveAsync();

        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges)
    {
        var companies = await _repository.Company.GetAllCompaniesAsync(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids == null)
            throw new IdParametersBadRequestException();

        var companies =  await _repository.Company.GetCompaniesAsync(ids, trackChanges);

        if (ids.Count() != companies.Count())
            throw new CollectionbyIdsBadRequestException();
        
        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public async Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        var companyDto = _mapper.Map<CompanyDto>(company);
        
        return companyDto;
    }

    public async Task DeleteCompanyAsync(Guid companyId, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        _repository.Company.DeleteCompany(company);
        await _repository.SaveAsync();
    }

    public async Task UpdateCompanyAsync(Guid companyId, CompanyForUpdateDto companyForUpdateDto, bool trackChanges)
    {
        var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        _mapper.Map(companyForUpdateDto, company);
        await _repository.SaveAsync();
    }
}



