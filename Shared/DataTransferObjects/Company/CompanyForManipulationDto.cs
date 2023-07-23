using Shared.DataTransferObjects.Employee;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects.Company;

public abstract record CompanyForManipulationDto
{
    [Required(ErrorMessage = "Company name is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for company name is 60 characters")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Company address is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for company address is 60 characters")]
    public string? Address { get; init; }

    public string? Country { get; init; 
    }
    public IEnumerable<EmployeeForCreationDto>? Employees { get; init; }
};


