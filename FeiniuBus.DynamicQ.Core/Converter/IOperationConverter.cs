using System;
using System.Reflection;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Converter
{
    public interface IOperationConverter
    {
        bool CanConvert(ClientTypes clientType, Type entityType, string field, object value, QueryOperations operation,
            PropertyInfo property, QueryRelations relations);

        void Convert(Type entityType, string parameterName, string field, object value, QueryOperations operation,
            PropertyInfo property, QueryRelations relations);
    }
}