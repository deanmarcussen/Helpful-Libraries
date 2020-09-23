using OrchardCore.ContentManagement;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YesSql
{
    public static class QueryExtensions
    {
        /// <summary>
        /// Breaks the query up into pages and lists the page using the given zero-based index. If pageIndex is 0 and
        /// count is <see cref="int.MaxValue"/> then the whole query is listed.
        /// </summary>
        /// <param name="query">The query to paginate.</param>
        /// <param name="pageIndex">Zero-based index of the desired page.</param>
        /// <param name="count">The page size.</param>
        /// <returns>The desired page of the resulting <see cref="ContentItem"/> s.</returns>
        public static Task<IEnumerable<ContentItem>> PaginateAsync(this IQuery<ContentItem> query, int pageIndex = 0, int count = int.MaxValue)
        {
            if (pageIndex > 0) query = query.Skip(pageIndex * count);
            if (count < int.MaxValue) query = query.Take(count);
            return query.ListAsync();
        }

        /// <summary>
        /// Breaks the query up into pages and lists the page using the given zero-based index. If pageIndex is 0 and
        /// count is <see cref="int.MaxValue"/> then the whole query is listed.
        /// </summary>
        /// <typeparam name="TPart">
        /// The final results are converted into the <see cref="ContentPart"/> of this type.
        /// </typeparam>
        /// <param name="query">The query to paginate.</param>
        /// <param name="pageIndex">Zero-based index of the desired page.</param>
        /// <param name="count">The page size.</param>
        /// <returns>
        /// The desired page of the resulting <see cref="ContentItem"/> s converted into the desired ContentPart. Those
        /// that don't have it are discarded.
        /// </returns>
        public static async Task<IEnumerable<TPart>> PaginateAsync<TPart>(this IQuery<ContentItem> query, int pageIndex = 0, int count = int.MaxValue)
            where TPart : ContentPart
        {
            return (await PaginateAsync(query, pageIndex, count)).As<TPart>().Where(part => part != null);
        }
    }
}