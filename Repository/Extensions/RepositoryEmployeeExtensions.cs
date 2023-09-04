using Entities.Models;
using System.Reflection;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

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

    public static IQueryable<Employee> Sort(this IQueryable<Employee> employees, string? orderByParameter)
    {

        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Employee>(orderByParameter);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return employees.OrderBy(x => x.Name);

        return employees.OrderBy(orderQuery);

    }

}
