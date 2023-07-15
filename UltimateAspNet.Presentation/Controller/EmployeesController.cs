using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects.Employee;

namespace UltimateAspNet.Presentation.Controller;

[ApiController]
[Route("api/companies/{companyId:guid}/employees")]
public class EmployeesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public EmployeesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public IActionResult GetEmployeesForCompany(Guid companyId)
    {
        var employees = _serviceManager.EmployeeService.GetEmployees(companyId, trackChanges: false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = nameof(GetEmployee))]
    public IActionResult GetEmployee(Guid companyId, Guid Id)
    {
        var employee = _serviceManager.EmployeeService.GetEmployee(companyId, Id, trackChanges: false);

        return Ok(employee);
    }

    [HttpPost]
    public IActionResult CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employeeForCreationDto)
    {

        if (employeeForCreationDto == null)
        {
            return BadRequest($"{nameof(employeeForCreationDto)} object is null");
        }

        var employeeDto = _serviceManager.EmployeeService.CreateEmployee(companyId, employeeForCreationDto, trackChanges: false);

        return CreatedAtAction(nameof(GetEmployee), new { companyId, id = employeeDto.Id}, employeeDto);
    }
}