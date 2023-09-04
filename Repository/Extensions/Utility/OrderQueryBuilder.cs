using Entities.Models;
using System.Reflection;
using System.Text;

namespace Repository.Extensions.Utility;

public static class OrderQueryBuilder
{
    public static string? CreateOrderQuery<T>(string? orderByParameter)
    {
        if (string.IsNullOrWhiteSpace(orderByParameter))
            return null;

        var orderParams = orderByParameter.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(' ')[0].Trim();

            var objectProperty = propertyInfos.FirstOrDefault(x => x.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var direction = param.EndsWith("desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name} {direction}");
        }

        return orderQueryBuilder.ToString().TrimEnd(',', ' ');
    }
}
