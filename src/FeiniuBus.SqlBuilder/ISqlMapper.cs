using System;

namespace FeiniuBus.SqlBuilder
{
    public interface ISqlMapper
    {
        void Mapping(SqlFieldMappings mappings);

        void Mapping(Action<SqlFieldMappings> mappings);
    }
}