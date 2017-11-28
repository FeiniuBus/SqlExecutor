using System.Collections.Generic;
using Newtonsoft.Json;

namespace FeiniuBus.DynamicQ
{
    public class QueryResult<T>
    {
        public QueryResult(int total, IEnumerable<T> rows)
        {
            Total = total;
            Rows = rows;
        }

        [JsonProperty("total")]
        public int Total { get; }

        [JsonProperty("rows")]
        public IEnumerable<T> Rows { get; }
    }

    public class QueryResult : QueryResult<object>
    {
        public QueryResult(int total, IEnumerable<object> rows) : base(total, rows)
        {
        }
    }

    public class PageResult<T> : QueryResult<T>
    {
        public PageResult(int total, IEnumerable<T> rows) : base(total, rows)
        {
        }
    }

    public class PageResult : QueryResult
    {
        public PageResult(int total, IEnumerable<object> rows) : base(total, rows)
        {
        }
    }
}