using System;

namespace FeiniuBus.LinqBuilder
{
    internal class MethodConverter : IMethodConverterProvider
    {
        public bool Match(DynamicQueryParam queryParam, Type propertyType)
        {
            return true;
        }

        public void Converter(DynamicQueryParam queryParam, Type propertyType, DynamicQueryKeyValueCollection collection)
        {
            var conValue = ConverterHelper.ChangeType(queryParam.Value, propertyType);
            var filed = queryParam.Field;
            var whereParams = collection.Values;
            var whereStringBuilder = collection.Builder;
            switch (queryParam.Operator)
            {
                case QueryOperation.Equal:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed} = @{collection.Ids++}");
                    break;
                case QueryOperation.LessThan:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed} < @{collection.Ids++}");
                    break;
                case QueryOperation.LessThanOrEqual:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed} <= @{collection.Ids++}");
                    break;
                case QueryOperation.GreaterThan:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed} > @{collection.Ids++}");
                    break;
                case QueryOperation.GreaterThanOrEqual:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed} >= @{collection.Ids++}");
                    break;
                case QueryOperation.Contains:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed}.Contains(@{collection.Ids++})");
                    break;
                case QueryOperation.StartsWith:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed}.StartsWith(@{collection.Ids++})");
                    break;
                case QueryOperation.EndsWith:
                    whereParams.Add(conValue);
                    whereStringBuilder.Append($"{filed}.EndsWith(@{collection.Ids++})");
                    break;
                default:
                    throw new ArgumentException($"{nameof(QueryOperation)}无效");
            }
        }
    }
}