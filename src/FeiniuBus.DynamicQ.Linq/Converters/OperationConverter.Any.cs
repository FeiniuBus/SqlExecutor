using FeiniuBus.AspNetCore.Json;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed class OperationAnyConverter : ParamGroupConverterInjectedOperationConverter
    {
        public OperationAnyConverter(LinqContext context,
            IEnumerable<IRelationConverter> relationConverters) : base(context, relationConverters)
        {
        }


        public override Convertable CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            if (clientType != ClientTypes.EntityFramework || operation != QueryOperations.Any)
                return false;

            if (!property.PropertyType.IsConstructedGenericType || !property.PropertyType.GetGenericArguments().Any())
                return "Syntax 'Any' only can use on the GenericType.";

            return true;
        }

        public override void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            var group = FeiniuBusJsonConvert.DeserializeObject<DynamicQueryParamGroup>((value ?? "").ToString());
            if (group == null || group.ChildGroups == null)
                return;
            Context.Parameters.Statement.Append($"{property.Name}.Any(");
            ParameterGroupConverter.Convert(property.PropertyType.GetGenericArguments().First(), group, relations);
            Context.Parameters.Statement.Append(")");
        }
    }
}