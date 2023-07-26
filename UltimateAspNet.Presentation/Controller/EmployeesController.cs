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
    public async Task<IActionResult> GetEmployeesForCompany(Guid companyId)
    {
        var employees = await _serviceManager.EmployeeService.GetEmployeesAsync(companyId, trackChanges: false);

        return Ok(employees);
    }

    [HttpGet("{id:guid}", Name = nameof(GetEmployee))]
    public async Task<IActionResult> GetEmployee(Guid companyId, Guid Id)
    {
        var employee = await _serviceManager.EmployeeService.GetEmployeeAsync(companyId, Id, trackChanges: false);

        return Ok(employee);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(Guid companyId, [FromBody] EmployeeForCreationDto employeeForCreationDto)
    {

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        if (employeeForCreationDto == null)
            return BadRequest($"{nameof(employeeForCreationDto)} object is null");
        

        var employeeDto = await _serviceManager.EmployeeService.CreateEmployeeForCompanyAsync(companyId, employeeForCreationDto, trackChanges: false);

        return CreatedAtAction(nameof(GetEmployee), new { companyId, id = employeeDto.Id}, employeeDto);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid companyId,  Guid id)
    {
        await _serviceManager.EmployeeService.DeleteEmployeeForCompanyAsync(companyId, id, trackChanges: false);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid companyId, Guid id, [FromBody] EmployeeForUpdateDto employeeForUpdateDto)
    {
        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.EmployeeService.UpdateEmployeeForCompanyAsync(companyId, id, employeeForUpdateDto, false, true);

        return NoContent();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> PartialyUpdateEmployee(Guid companyId, Guid id, [FromBody] JsonPatchDocument<EmployeeForUpdateDto> patchDoc)
    {
        if (patchDoc == null) { return BadRequest("patchDoc object sent from client is null"); }

        var (employeeToPatch, employeeEntity) = await _serviceManager.EmployeeService.GetEmployeeForPatchAsync(companyId, id, companyTrackChanges: false, employeeTrackChanges: true);

        patchDoc.ApplyTo(employeeToPatch, ModelState);

        TryValidateModel(ModelState);

        if (!ModelState.IsValid)
            return UnprocessableEntity(ModelState);

        await _serviceManager.EmployeeService.SaveChangesForPatchAsync(employeeToPatch, employeeEntity);

        return NoContent();
    }
}