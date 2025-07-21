
using Microsoft.EntityFrameworkCore;

namespace CMS.Application.Common.GenericObjects
{
    public class PaginatedList<T> where T : class
    {
        public List<T>? Data { get; set; }
        public Pagination? Pagination { get; set; }
    }


    public class Pagination
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; } = 0;
        public int TotalRecords { get; set; } = 0;
        public int PageSize = 25;
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
    }

    public static class CreatePaginatedList
    {
        public static async Task<PaginatedList<T>> CreateAsync<T>(this IQueryable<T> source, int PageIndex, int PageSize) where T : class
        {
            var TotalRecords = await source.CountAsync();
            var data = await source.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToListAsync();
            int TotalPages = (int)Math.Ceiling(TotalRecords / (double)PageSize);

            Pagination? pagination = new()
            {
                PageIndex = PageIndex,
                TotalPages = TotalPages,
                TotalRecords = TotalRecords,
                PageSize = PageSize,
                HasPreviousPage = PageIndex > 1,
                HasNextPage = PageIndex < TotalPages
            };

            var returnData = new PaginatedList<T>();
            returnData.Data = data;
            returnData.Pagination = pagination;

            return returnData;
        }
    }
}