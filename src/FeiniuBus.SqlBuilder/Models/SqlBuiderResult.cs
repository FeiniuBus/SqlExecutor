using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public class SqlBuiderResult
    {
        public SqlBuiderResult()
        {
            SqlString = new StringBuilder();
            Params = new Dictionary<string, object>();
        }
        public StringBuilder SqlString { get; set; }
        public IDictionary<string, object> Params { get; set; }
        public override string ToString()
        {
            return SqlString?.ToString() ?? string.Empty;
        }
    }
}
