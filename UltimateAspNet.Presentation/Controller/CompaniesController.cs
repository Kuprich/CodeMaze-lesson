using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Company;
using UltimateAspNet.Presentation.ActionFilters;
using UltimateAspNet.Presentation.ModelBinders;

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
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _serviceManager.CompanyService.GetAllCompaniesAsync(trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("collection/({ids})", Name = nameof(GetCompanies))]
    public async Task<IActionResult> GetCompanies([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids)
    {
        var companies = await _serviceManager.CompanyService.GetCompaniesAsync(ids, trackChanges: false);

        return Ok(companies);
    }

    [HttpGet("{id:guid}", Name = nameof(GetCompany))]
    public async Task<IActionResult> GetCompany(Guid id)
    {
        var company = await _serviceManager.CompanyService.GetCompanyAsync(id, trackChanges: false);

        return Ok(company);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompany([FromBody] CompanyForCreationDto companyForCreationDto)
    {
        var company = await _serviceManager.CompanyService.CreateCompanyAsync(companyForCreationDto);

        return CreatedAtRoute(nameof(GetCompany), new { company.Id }, company);
    }

    [HttpPost("collection")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> CreateCompanies([FromBody] IEnumerable<CompanyForCreationDto> CompaniesForCreationDto)
    {
        var companies = await _serviceManager.CompanyService.CreateCompaniesAsync(CompaniesForCreationDto);

        var ids = companies.Select(company => company.Id).ToList();

        return CreatedAtRoute(nameof(GetCompanies), ids, companies);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleleteCompany(Guid id)
    {
        await _serviceManager.CompanyService.DeleteCompanyAsync(id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [ServiceFilter(typeof(ValidationFilterAttribute))]
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto companyForUpdateDto)
    {
        await _serviceManager.CompanyService.UpdateCompanyAsync(id, companyForUpdateDto, trackChanges: true);

        return NoContent();
    }
}
