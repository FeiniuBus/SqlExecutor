using System;
using System.Collections.Generic;
using System.Text;

namespace FeiniuBus.SqlBuilder
{
    public interface ISqlMapper
    {
        void Mapping(SqlFieldMappings mappings);

        void Mapping(Action<SqlFieldMappings> mappings);
    }
}
