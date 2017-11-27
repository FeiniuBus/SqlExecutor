using System;

namespace FeiniuBus.SqlBuilder
{
    public class SqlMapper : ISqlMapper
    {
        protected readonly SqlFieldMappings SqlFieldMappings;

        public SqlMapper()
        {
            SqlFieldMappings = new SqlFieldMappings();
        }

        public virtual void Mapping(SqlFieldMappings mappings)
        {
            SqlFieldMappings.AddRange(mappings);
        }

        public virtual void Mapping(Action<SqlFieldMappings> mappings)
        {
            mappings.Invoke(SqlFieldMappings);
        }
    }
}