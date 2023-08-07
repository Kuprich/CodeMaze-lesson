using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Shared.Extensions;

public static class PagedListExtensions
{
    public static async Task<PagedList<TSource>> ToPagedListAsync<TSource>(
           this IQueryable<TSource> source,
           int currentPage,
           int pageSize,
           CancellationToken cancellationToken = default)
    {

        var list = await source.Skip((pageSize - 1) * currentPage)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedList<TSource>(list, currentPage, pageSize);
    }
}
