using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed class OperationEqualConverter : BaseOperationConverter
    {
        public OperationEqualConverter(LinqContext context, IEnumerable<IRelationConverter> relationConverters) : base(
            context, relationConverters)
        {
        }

        public override bool CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            return clientType == ClientTypes.EntityFramework && operation == QueryOperations.Equal;
        }

        public override void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            var clause = $"{field} == @{parameterName})";
            var clause1 = clause;
            var relationConverter = RelationConverters.FirstOrDefault(x => x.CanConvert(ClientTypes.EntityFramework,
                entityType, relations));
            if (relationConverter == null)
                throw new Exception($"Converter '{relations}' for {ClientTypes.EntityFramework} not found.");
            clause = relationConverter.Convert(entityType, relations, Context.Parameters.CurrentIndex(), clause);
            Context.Parameters.Statement.Append(clause);
            Context.Parameters.Values.Add(value.ChangeType(property.PropertyType));
        }
    }
}