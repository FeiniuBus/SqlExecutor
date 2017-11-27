using System;

namespace FeiniuBus.SqlBuilder
{
    public interface IWhereBuilder : ISqlMapper
    {
        void Where(DynamicQueryParamGroup query, bool fieldConverter = true);
        void Where(Action<DynamicQueryParamGroupBuilder> queryBuilder, bool fieldConverter = true);
        SqlBuiderResult BuildWhere();
    }
}