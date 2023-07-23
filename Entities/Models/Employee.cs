using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Employee
{
    [Column("EmployeeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Employee name is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for Employee name is 60 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Employee age is required field")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Employee position is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for employee position is 60 characters")]
    public string? Position { get; set; }

    [ForeignKey(nameof(Company))]
    public Guid CompanyId { get; set; }
    public Company? Company { get; set;}
}