using System;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public class RelationAndConverter : IRelationConverter
    {
        public Convertable CanConvert(ClientTypes clientType, Type entityType, QueryRelations relationt)
        {
            return clientType == ClientTypes.EntityFramework && relationt == QueryRelations.And;
        }

        public string Convert(Type entityType, QueryRelations relation, int index, string statement)
        {
            return $"{GetPrefix(entityType, relation, index)}{statement}{GetSuffix(entityType, relation, index)}";
        }

        public string GetPrefix(Type entityType, QueryRelations relation, int index)
        {
            return index == 0 ? "(" : " && (";
        }

        public string GetSuffix(Type entityType, QueryRelations relation, int index)
        {
            return ")";
        }
    }
}