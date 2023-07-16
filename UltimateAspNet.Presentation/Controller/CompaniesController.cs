using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Company;

namespace UltimateAspNet.Presentation.Controller;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CompaniesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _serviceManager.CompanyService.GetAllCompanies(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("collection/({ids})", Name = nameof(GetCompanies))]
    public IActionResult GetCompanies(IEnumerable<Guid> ids)
    {
        var companies = _serviceManager.CompanyService.GetCompanies(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = nameof(GetCompany))]
    public IActionResult GetCompany(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);

        return Ok(company);
    }
    
    [HttpPost]
    public IActionResult CreateCompany([FromBody]CompanyForCreationDto companyForCreationDto)
    {
        //if (companyForCreationDto == null)
        //    return BadRequest($"{nameof(companyForCreationDto)} object is null.");

        var company = _serviceManager.CompanyService.CreateCompany(companyForCreationDto);

        return CreatedAtRoute(nameof(GetCompany), new {company.Id}, company);
    }

    [HttpPost("collection")]
    public IActionResult CreateCompanies([FromBody] IEnumerable<CompanyForCreationDto> CompaniesForCreationDto)
    {
        var companies = _serviceManager.CompanyService.CreateCompanies(CompaniesForCreationDto);

        var ids = companies.Select(company => company.Id).ToList();

        return CreatedAtRoute(nameof(GetCompanies), ids, companies);
    }
}
