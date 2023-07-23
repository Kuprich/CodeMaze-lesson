using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Employee;

public abstract record EmployeeForManipulationDto
{
    [Required(ErrorMessage = "Employee name is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for Employee name is 60 characters")]
    public string? Name { get; init; }

    [Range(14, 100, ErrorMessage = "Age is required and it can't be lower than 14")]
    public int Age { get; init; }

    [Required(ErrorMessage = "Employee position is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for employee position is 60 characters")]
    public string? Position { get; init; }
};


