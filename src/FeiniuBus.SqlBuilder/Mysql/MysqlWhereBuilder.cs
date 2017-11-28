using System;
using FeiniuBus.Converter;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlWhereBuilder : SqlMapper, IWhereBuilder
    {
        private readonly MysqlConverterHelper _mysqlConverterHelper;
        private DynamicQueryParamGroup _dynamicQueryParamGroup;

        public MysqlWhereBuilder(ICharacterConverter characterConverter)
        {
            _mysqlConverterHelper = new MysqlConverterHelper();
            CharacterConverter = characterConverter;
        }

        protected ICharacterConverter CharacterConverter { get; set; }

        public void Where(DynamicQueryParamGroup query, bool fieldConverter = true)
        {
            var queryModel = query;
            if (fieldConverter)
                queryModel = CharacterConverter.ConverterDynamicQueryParamGroup(query);
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
                return new SqlBuiderResult();
            _mysqlConverterHelper.ConverterQueryParamGroup(_dynamicQueryParamGroup, res, SqlFieldMappings, null);
            return res;
        }
    }
}