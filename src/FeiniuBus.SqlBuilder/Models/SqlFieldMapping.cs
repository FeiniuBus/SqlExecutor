using System;
using System.Collections.Generic;

namespace FeiniuBus.SqlBuilder
{
    public class SqlFieldMapping
    {
        public string Key { get; set; }
        public string SqlField { get; set; }
        public Type Type { get; set; }
    }

    public class SqlFieldMappings : List<SqlFieldMapping>
    {
        public void Add(string key, string sqlfield, Type type = null)
        {
            Add(new SqlFieldMapping {Key = key, SqlField = sqlfield, Type = type});
        }
    }
}