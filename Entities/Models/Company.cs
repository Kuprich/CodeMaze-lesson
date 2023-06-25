﻿using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Company
{
    public Guid CompanyId { get; set; }

    [Required(ErrorMessage = "\"Company name\" is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for \"company name\" is 60 characters")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "\"Company address\" is required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for \"company address\" is 60 characters")]
    public string? Address { get; set; }

    public string? Country { get; set; }

    public ICollection<Employee>? Employees { get; set; }
}
