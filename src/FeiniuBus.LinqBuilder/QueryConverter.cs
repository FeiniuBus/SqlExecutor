using System;
using System.Linq;

namespace FeiniuBus.LinqBuilder
{
    public class QueryConverter
    {
        private readonly Type _entityType;
        private readonly DynamicQuery _queryModel;

        public QueryConverter(Type entityType, DynamicQuery queryModel, bool characterConverter = false)
        {

            _queryModel = characterConverter ? DynamicQueryCharacterConverter.Converter(queryModel) : queryModel;
            _entityType = entityType;
        }

        private DynamicQueryKeyValueCollection ConverterWhere()
        {
            var collection = new DynamicQueryKeyValueCollection();
            if (_queryModel.ParamGroup == null)
                return collection;
            ConverterHelper.ConverterQueryParamGroup(_entityType, _queryModel.ParamGroup, collection);
            return collection;
        }


        private string ConverterSelect()
        {
            return ConverterHelper.ConverterSelect(_queryModel.Select);
        }

        public DynamicQueryModel Converter()
        {
            var result = new DynamicQueryModel
            {
                IsPager = _queryModel.Pager,
                Skip = _queryModel.Skip < 0 ? 0 : _queryModel.Skip,
                Take = _queryModel.Take < 0 ? 10 : _queryModel.Take
            };
            var w = ConverterWhere();
            result.Where = w.Builder.ToString();
            result.WhereValue = w.Values.ToArray();
            result.IsPager = _queryModel.Pager;
            result.Order = ConverterHelper.ConverterOrderBy(_queryModel.Order);
            result.Select = ConverterSelect();
            return result;
        }
    }
}