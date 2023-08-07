namespace Shared.RequestFeatures;

public class PagedList<T> : List<T>
{
    public MetaData MetaData { get; set; }
    public PagedList(List<T> items, int currentPage, int pageSize)
    {
        MetaData = new()
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = items.Count,
            TotalPages = (int)Math.Ceiling(items.Count / (double)pageSize)
        };

        AddRange(items);
    }

    public PagedList(IEnumerable<T> items, MetaData metaData)
    {
        MetaData = metaData;
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(List<T> source, int currentPage, int pageSize)
    {
        var items = source
            .Skip((pageSize - 1) * currentPage)
            .Take(pageSize)
            .ToList();

        return new PagedList<T>(items, currentPage, pageSize);
    }



}