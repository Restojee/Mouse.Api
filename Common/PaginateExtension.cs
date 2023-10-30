using Microsoft.EntityFrameworkCore;

namespace Mouse.NET.Common;

using System;
using System.Linq;

public static class PaginationExtensions
{
    public static async Task<PagedResult<T>> ToPagedResult<T>(this IQueryable<T> source, int page, int pageSize)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize > 100)
        {
            pageSize = 100;
        }
        
        if (pageSize < 1)
        {
            pageSize = 1;
        }

        var items = await source.Skip((page - 1) * pageSize)
            .Take(pageSize)
            .AsQueryable()
            .ToListAsync();

        var totalItems = await source.CountAsync();
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        if (page > totalPages)
        {
            page = totalPages;
        }

        return new PagedResult<T>(items, page, pageSize, totalItems, totalPages);
    }
}

public class PagedResult<T>
{
    public int Page { get; }
    public int PageSize { get; }
    public int TotalItems { get; }
    public int TotalPages { get; }
    public ICollection<T> Records { get; }

    public PagedResult(ICollection<T> records, int page, int pageSize, int totalItems, int totalPages)
    {
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = totalPages;
        Records = records;
    }
}