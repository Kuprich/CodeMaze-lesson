using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryEmployeeExtensions
{
    public static IQueryable<Employee> FilterEmployeesByAge(this IQueryable<Employee> employees, int minAge, int maxAge) => employees
            .Where(e => e.Age >= minAge && e.Age <= maxAge);

    public static IQueryable<Employee> SearchByName(this IQueryable<Employee> employees, string? searchTerm)
    {
        if (searchTerm == null)
            return employees;

        return employees
            .Where(e => !string.IsNullOrWhiteSpace(e.Name) && e.Name.ToLower().Contains(searchTerm.ToLower()) == true);
    }

}
