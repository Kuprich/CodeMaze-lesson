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

    public CompanyDto CreateCompany(CompanyForCreationDto companyForCreationDto)
    {
        if (companyForCreationDto == null)
            throw new CompanyBadRequestException();
        var company = _mapper.Map<Company>(companyForCreationDto);

        _repository.Company.CreateCompany(company);
        _repository.Save();

        return _mapper.Map<CompanyDto>(company);
    }

    public IEnumerable<CompanyDto> CreateCompanies(IEnumerable<CompanyForCreationDto> companiesForCreationDto)
    {
        if (companiesForCreationDto == null)
            throw new CompaniesBadRequestException();
        var companies = _mapper.Map<IEnumerable<Company>>(companiesForCreationDto);
        _repository.Company.CreateCompanies(companies);
        _repository.Save();

        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = _repository.Company.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);

        return companiesDto;
    }

    public IEnumerable<CompanyDto> GetCompanies(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids == null)
            throw new IdParametersBadRequestException();
        

        var companies =  _repository.Company.GetCompanies(ids, trackChanges);

        if (ids.Count() != companies.Count())
            throw new CollectionbyIdsBadRequestException();
        
        return _mapper.Map<IEnumerable<CompanyDto>>(companies);
    }

    public CompanyDto GetCompany(Guid companyId, bool trackChanges)
    {
        var company = (_repository.Company?.GetCompany(companyId, trackChanges)) 
            ?? throw new CompanyNotFoundException(companyId);

        var companyDto = _mapper.Map<CompanyDto>(company);
        
        return companyDto;
    }

    public void DeleteCompany(Guid companyId, bool trackChanges)
    {
        var company = _repository.Company.GetCompany(companyId, trackChanges)
            ?? throw new CompanyNotFoundException(companyId);

        _repository.Company.DeleteCompany(company);
        _repository.Save();
    }
}



