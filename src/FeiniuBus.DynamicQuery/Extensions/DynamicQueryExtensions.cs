using System;
using System.Collections.Generic;
using System.Linq;

namespace FeiniuBus
{
    public static class DynamicQueryExtensions
    {
        public static DynamicQuery SetDefaultOrderBy(this DynamicQuery dynamicQuery, string field,
            ListSortDirection sort = ListSortDirection.Ascending)
        {
            if (dynamicQuery == null)
                throw new ArgumentNullException(nameof(dynamicQuery));
            if (string.IsNullOrWhiteSpace(field))
                throw new ArgumentNullException(nameof(dynamicQuery));
            if (dynamicQuery.Order == null)
                dynamicQuery.Order = new List<DynamicQueryOrder>();
            if (!dynamicQuery.Order.Any())
                dynamicQuery.Order.Add(new DynamicQueryOrder {Name = field, Sort = sort});
            return dynamicQuery;
        }
    }
}