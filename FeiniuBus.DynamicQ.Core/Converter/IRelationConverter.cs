using System;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Converter
{
    public interface IRelationConverter
    {
        bool CanConvert(ClientTypes clientType, Type entityType, QueryRelations relationt);
        string Convert(Type entityType, QueryRelations relation, int index, string statement);
        string GetPrefix(Type entityType, QueryRelations relation, int index);
        string GetSuffix(Type entityType, QueryRelations relation, int index);
    }
}