using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FeiniuBus.LinqBuilder
{
    internal class MethodConverterByIn : IMethodConverterProvider
    {
        public bool Match(DynamicQueryParam queryParam, Type propertyType)
        {
            return queryParam.Operator == QueryOperation.In;
        }

        public void Converter(DynamicQueryParam queryParam, Type propertyType,
            DynamicQueryKeyValueCollection collection)
        {
            if (string.IsNullOrWhiteSpace(queryParam.Value?.ToString()))
                throw new ArgumentException("IN操作必须提供Value.");
            var arr = queryParam.Value.ToString()
                .Trim()
                .Split(',')
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => ConverterHelper.ChangeType(i, propertyType)).ToList();

            var listType = typeof(List<>);
            var s = listType.MakeGenericType(propertyType);
            var list = Activator.CreateInstance(s) as IList;
            foreach (var o in arr)
                list.Add(o);
            object valObj = list;

            if (!arr.Any())
                throw new ArgumentException("IN操作必须提供Value.");
            collection.Builder.Append($" @{collection.Ids++}.Contains({queryParam.Field}) ");
            collection.Values.Add(valObj);
        }
    }
}