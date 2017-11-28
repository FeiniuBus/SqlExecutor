using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeiniuBus.AspNetCore.Json;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed class OperationAnyConverter : BaseOperationConverter
    {
        private readonly IEnumerable<IParameterGroupConverter> _groupConverters;

        public OperationAnyConverter(IEnumerable<IParameterGroupConverter> groupConverters, LinqContext context,
            IEnumerable<IRelationConverter> relationConverters) : base(context, relationConverters)
        {
            _groupConverters = groupConverters;
        }


        public override bool CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            return property.PropertyType.IsConstructedGenericType && property.PropertyType.GetGenericArguments().Any()
                ? clientType == ClientTypes.EntityFramework && operation == QueryOperations.Any
                : throw new Exception("只有泛型类型能够使用此方法");
        }

        public override void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            var group = FeiniuBusJsonConvert.DeserializeObject<DynamicQueryParamGroup>((value ?? "").ToString());
            if (group == null || group.ChildGroups == null)
                return;
            var groupConverter = _groupConverters.FirstOrDefault(x =>
                x.CanConvert(ClientTypes.EntityFramework, entityType, group, relations));
            if (groupConverter == null)
                throw new Exception($"Converter of '{relations}' for {ClientTypes.EntityFramework} not found.");
            Context.Parameters.Statement.Append("(1=1");
            groupConverter.Convert(entityType, group, relations);
            Context.Parameters.Statement.Append(")");
        }
    }
}