using System;
using FeiniuBus.Builder;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlWhereBuilder : SqlMapper, IWhereBuilder
    {
        private readonly MysqlConverterHelper _mysqlConverterHelper;
        private readonly ICharacterConverter _characterConverter;
        private DynamicQueryParamGroup _dynamicQueryParamGroup;

        public MysqlWhereBuilder(ICharacterConverter characterConverter)
        {
            _mysqlConverterHelper = new MysqlConverterHelper();
            _characterConverter = characterConverter;
        }
        public void Where(DynamicQueryParamGroup query, bool fieldConverter = true)
        {
            var queryModel = query;
            if (fieldConverter)
            {
                queryModel = _characterConverter.ConverterDynamicQueryParamGroup(query);
            }
            _dynamicQueryParamGroup = queryModel;
        }
        public void Where(Action<DynamicQueryParamGroupBuilder> queryBuilder, bool fieldConverter = true)
        {
            var buider = new DynamicQueryParamGroupBuilder();
            queryBuilder.Invoke(buider);
            var model = buider.Build();
            Where(model, fieldConverter);
        }

        public SqlBuiderResult BuildWhere()
        {
            var res = new SqlBuiderResult();
            if (_dynamicQueryParamGroup == null)
            {
                return new SqlBuiderResult();
            }
            _mysqlConverterHelper.ConverterQueryParamGroup(_dynamicQueryParamGroup, res, SqlFieldMappings, null);
            return res;
        }
    }
}
