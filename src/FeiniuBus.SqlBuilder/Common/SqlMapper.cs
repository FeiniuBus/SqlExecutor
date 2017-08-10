using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public class SqlMapper
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
