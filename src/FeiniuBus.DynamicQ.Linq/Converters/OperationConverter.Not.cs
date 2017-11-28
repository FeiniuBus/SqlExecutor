using FeiniuBus.AspNetCore.Json;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed  class OperationNotConverter : ParamGroupConverterInjectedOperationConverter
    {
        public OperationNotConverter(LinqContext context, IEnumerable<IRelationConverter> relationConverters) : base(context, relationConverters)
        {
        }

        public override Convertable CanConvert(ClientTypes clientType, Type entityType, string field, object value, QueryOperations operation,
            PropertyInfo property, QueryRelations relations)
        {
            return clientType == ClientTypes.EntityFramework && operation == QueryOperations.Not;
        }

        public override void Convert(Type entityType, string parameterName, string field, object value, QueryOperations operation,
            PropertyInfo property, QueryRelations relations)
        {
            var group = FeiniuBusJsonConvert.DeserializeObject<DynamicQueryParamGroup>((value ?? "").ToString());
            if (group == null || group.ChildGroups == null)
                return;
            Context.Parameters.Statement.Append("(");
            ParameterGroupConverter.Convert(entityType, group, relations);
            Context.Parameters.Statement.Append(")==false");
        }
    }
}
