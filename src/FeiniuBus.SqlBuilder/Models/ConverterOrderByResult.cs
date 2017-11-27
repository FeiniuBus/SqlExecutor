using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public class ConverterOrderByResult
    {
        public ConverterOrderByResult()
        {
            OrderByString = new StringBuilder();
            Params = new Dictionary<string, object>();
        }

        public StringBuilder OrderByString { get; set; }
        public IDictionary<string, object> Params { get; set; }

        public override string ToString()
        {
            return OrderByString?.ToString() ?? string.Empty;
        }
    }
}