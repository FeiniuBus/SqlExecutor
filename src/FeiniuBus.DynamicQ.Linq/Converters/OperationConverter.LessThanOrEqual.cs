using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed class OperationLessThanOrEqualConverter : BaseOperationConverter
    {
        public OperationLessThanOrEqualConverter(LinqContext context,
            IEnumerable<IRelationConverter> relationConverters) : base(context, relationConverters)
        {
        }

        public override Convertable CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            return clientType == ClientTypes.EntityFramework && operation == QueryOperations.LessThanOrEqual;
        }

        public override void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            var clause = $"{property.Name} <= @{parameterName}";
            var relationConverter = RelationConverters.FirstOrDefault(x => x.CanConvert(ClientTypes.EntityFramework,
                entityType, relations).ThrowIfCouldNotConvert());
            if (relationConverter == null)
                throw new Exception($"Converter '{relations}' for {ClientTypes.EntityFramework} not found.");
            clause = relationConverter.Convert(entityType, relations, Context.Parameters.CurrentIndex(), clause);
            Context.Parameters.Statement.Append(clause);
            Context.Parameters.Values.Add(value.ChangeType(property.PropertyType));
        }
    }
}