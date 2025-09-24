using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoGo.Application.Common.Pagination
{
    public static class IEnumrableExtensions
    {
        public static async Task<PaginationResult<T>> ToPagedResultAsync<T>(
            this IEnumerable<T> query,
            int pageNumber,
            int pageSize)
        {
            var totalCount = query.Count();

            var items =  query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return new PaginationResult<T>(items, totalCount, pageNumber, pageSize);
        }
    }
}
