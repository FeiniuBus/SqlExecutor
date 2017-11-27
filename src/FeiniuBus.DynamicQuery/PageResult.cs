using System.Collections.Generic;
using Newtonsoft.Json;

namespace FeiniuBus
{
    public class PageResult
    {
        [JsonProperty("rows")]
        public object Rows { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }

    public class PageResult<T> : PageResult
    {
        [JsonProperty("rows")]
        public new IEnumerable<T> Rows { get; set; }
    }
}