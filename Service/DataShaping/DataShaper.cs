using Contracts;
using System.Dynamic;
using System.Reflection;

namespace Service.DataShaping;

public class DataShaper<T> : IDataShaper<T> where T : class
{
    private PropertyInfo[] _allProperties { get; set; }
    public DataShaper()
    {
        _allProperties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);
    }

    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? propertiesString)
    {
        var properties = GetProperties(propertiesString);

        return FetchData(entities, properties);
    }

    public ExpandoObject ShapeData(T entity, string? propertiesString)
    {
        var properties = GetProperties(propertiesString);

        return FetchDataForEntity(entity, properties);
    }

    private IEnumerable<PropertyInfo> GetProperties(string? propertiesString)
    {
        if (string.IsNullOrWhiteSpace(propertiesString))
            return _allProperties;

        var propertyNames = propertiesString.Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(p => p.ToLower());

        return _allProperties.Where(prop => propertyNames.Contains(prop.Name.ToLower()));
    }

    private static ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> properties)
    {
        var shapedObject = new ExpandoObject();

        foreach (var property in properties)
            shapedObject.TryAdd(property.Name, property.GetValue(entity));

        return shapedObject;
    }

    private static IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> properties)
    {
        var shapedObjects = new List<ExpandoObject>();

        foreach (var entity in entities)
        {
            shapedObjects.Add(FetchDataForEntity(entity, properties));
        }

        return shapedObjects;
    }
}
