namespace Shared.RequestFeatures;

public class EmployeeParameters : RequestParameters
{
    public int MinAge { get; set; }
    public int MaxAge { get; set; } = int.MaxValue;

    public bool AgeIsValid => MinAge < MaxAge;
}