using Microsoft.AspNetCore.JsonPatch;
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

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (employeeForCreationDto == null)
            return BadRequest($"{nameof(employeeForCreationDto)} object is null");
        

        var employeeDto = _serviceManager.EmployeeService.CreateEmployeeForCompany(companyId, employeeForCreationDto, trackChanges: false);

        return CreatedAtAction(nameof(GetEmployee), new { companyId, id = employeeDto.Id}, employeeDto);
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployee(Guid companyId,  Guid id)
    {
        _serviceManager.EmployeeService.DeleteEmployeeForCompany(companyId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateEmployee(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _serviceManager.EmployeeService.UpdateEmployeeForCompany(companyId, id, employeeForUpdateDto, false, true);

        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public IActionResult PartialyUpdateEmployee(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
    {
        if (patchDoc == null) { return BadRequest("patchDoc object sent from client is null"); }

        var (employeeToPatch, employeeEntity) = _serviceManager.EmployeeService.GetEmployeeForPatch(companyId, id, companyTrackChanges: false, employeeTrackChanges: true);

        patchDoc.ApplyTo(employeeToPatch, ModelState);

        TryValidateModel(ModelState);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        _serviceManager.EmployeeService.SaveChangesForPatch(employeeToPatch, employeeEntity);

        return NoContent();
    }
}