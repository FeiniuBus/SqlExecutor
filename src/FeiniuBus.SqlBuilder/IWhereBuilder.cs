using System;
using System.Collections.Generic;
using System.Text;
using FeiniuBus.Builder;

namespace FeiniuBus.SqlBuilder
{
    public interface IWhereBuilder : ISqlMapper
    {
        void Where(DynamicQueryParamGroup query, bool fieldConverter = true);

        void Where(Action<DynamicQueryParamGroupBuilder> queryBuilder, bool fieldConverter = true);



        SqlBuiderResult BuildWhere();
    }
}
