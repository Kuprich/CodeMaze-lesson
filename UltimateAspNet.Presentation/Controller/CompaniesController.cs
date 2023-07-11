using Microsoft.AspNetCore.Mvc;
using Service.Contracts;

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

    [HttpGet("{id:guid}")]
    public IActionResult GetCompanies(Guid id)
    {
        var company = _serviceManager.CompanyService.GetCompany(id, trackChanges: false);
        return Ok(company);
    }
}
