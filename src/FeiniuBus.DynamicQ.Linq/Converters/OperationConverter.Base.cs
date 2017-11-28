using System;
using System.Collections.Generic;
using System.Reflection;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public abstract class BaseOperationConverter : IOperationConverter
    {
        protected BaseOperationConverter(LinqContext context, IEnumerable<IRelationConverter> relationConverters)
        {
            Context = context;
            RelationConverters = relationConverters;
        }

        protected LinqContext Context { get; }
        protected IEnumerable<IRelationConverter> RelationConverters { get; }

        public abstract bool CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations);

        public abstract void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations);
    }
}