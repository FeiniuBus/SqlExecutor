using FeiniuBus.LinqBuilder;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
        public static PageResult<TEntity> AutoQueryAsync<TEntity>(IQueryable<TEntity> source,DynamicQueryModel dynamicQuery, bool count)
        {
            var s = source;
            if (!string.IsNullOrWhiteSpace(dynamicQuery.Where))
                s = source.Where(dynamicQuery.Where, dynamicQuery.WhereValue.ToArray());
            if (!string.IsNullOrWhiteSpace(dynamicQuery.Order))
                s = s.OrderBy(dynamicQuery.Order);
            if (!dynamicQuery.IsPager)
            {
                if (string.IsNullOrWhiteSpace(dynamicQuery.Select))
                    return new PageResult<TEntity>() { Rows = s.ToList() };
                return new PageResult<TEntity>() { Rows = source.Select(dynamicQuery.Select).Cast<TEntity>().ToList() };
            }

            var pageResult = new PageResult<TEntity>
            {
                Total = count ? s.Count() : 0
            };

            if (string.IsNullOrWhiteSpace(dynamicQuery.Select))
                pageResult.Rows = s
                    .Skip(dynamicQuery.Skip < 0 ? 0 : dynamicQuery.Skip)
                    .Take(dynamicQuery.Take < 0 ? 10 : dynamicQuery.Take)
                    .ToList();
            else
                pageResult.Rows = (s.Select(dynamicQuery.Select)
                    .Skip(dynamicQuery.Skip < 0 ? 0 : dynamicQuery.Skip)
                    .Take(dynamicQuery.Take < 0 ? 10 : dynamicQuery.Take)).Cast<TEntity>().ToList();
            return pageResult;
        }
    }
}
