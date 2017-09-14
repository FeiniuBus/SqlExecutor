using System;

namespace FeiniuBus.LinqBuilder
{
    class MethodConverterByDataTimeLessThanOrEqualThenDay : IMethodConverterProvider
    {
        public bool Match(DynamicQueryParam queryParam, Type propertyType)
        {
            return queryParam.Operator == QueryOperation.DataTimeLessThanOrEqualThenDay &&
                   propertyType == typeof(DateTime);
        }

        public void Converter(DynamicQueryParam queryParam, Type propertyType, DynamicQueryKeyValueCollection collection)
        {
            DateTime datetime;
            if (!DateTime.TryParse(queryParam.Value?.ToString(), out datetime))
            {
                throw new ArgumentException($"{queryParam.Value}不是有效的DateTime类型");
            }
            queryParam.Value = new DateTime(datetime.Year, datetime.Month, datetime.Day).AddDays(1).AddMilliseconds(-1);
            MethodProviderCollection.DefaultConverterProvider.Converter(queryParam, propertyType, collection);
        }
    }
}
