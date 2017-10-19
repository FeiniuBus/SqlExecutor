using System;

namespace FeiniuBus.SqlBuilder
{
    public static class SqlFieldMappingsExtensions
    {
        public static SqlFieldMappings Map(this SqlFieldMappings mappings, string key, string sqlfield, Type type = null)
        {
            mappings.Add(key, sqlfield, type);
            return mappings;
        }
    }
}
