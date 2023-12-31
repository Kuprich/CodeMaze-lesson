﻿using Entities.Exceptions;

namespace Entities.Exceptions;

public abstract class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message)
    { }
}

public class IdParametersBadRequestException : BadRequestException
{
    public IdParametersBadRequestException()
        : base("Parameter Ids is null")
    { }
}

public class CollectionbyIdsBadRequestException : BadRequestException
{
    public CollectionbyIdsBadRequestException()
        : base("Collection count mismatch comparing to Ids")
    { }
}

public class CompanyBadRequestException : BadRequestException
{
    public CompanyBadRequestException()
        : base("Parameter Company is null")
    { }
}

public class CompaniesBadRequestException : BadRequestException
{
    public CompaniesBadRequestException()
        : base("Parameter Companies is null")
    { }
}

public class EmployeeBadRequestException : BadRequestException
{
    public EmployeeBadRequestException() 
        : base("Parameter Employee is null")
    { }
}

public class MaxAgeRangeBadRequestException : BadRequestException
{
    public MaxAgeRangeBadRequestException()
        : base("Max age can't be less than min age")
    { }
}