using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public class OperationInConverter : BaseOperationConverter
    {
        public OperationInConverter(LinqContext context, IEnumerable<IRelationConverter> relationConverters) : base(
            context, relationConverters)
        {
        }

        public override Convertable CanConvert(ClientTypes clientType, Type entityType, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            if (clientType != ClientTypes.EntityFramework || operation != QueryOperations.In) return false;
            if (value == null || string.IsNullOrEmpty(value.ToString().Trim()))
                return "Syntax 'In' need at least two value.";
                //throw new ArgumentException("IN操作必须提供Value.");
            if (!value.ToString()
                .Trim()
                .Split(',')
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => i.ChangeType(property.PropertyType)).Any())
                return "Syntax 'In' need at least two value."; //throw new ArgumentException("IN操作必须提供Value.");
            return true;
        }

        public override void Convert(Type entityType, string parameterName, string field, object value,
            QueryOperations operation, PropertyInfo property, QueryRelations relations)
        {
            var arr = value.ToString()
                .Trim()
                .Split(',')
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .Select(i => i.ChangeType(property.PropertyType)).ToList();
            var listType = typeof(List<>);
            var s = listType.MakeGenericType(property.PropertyType);
            var list = Activator.CreateInstance(s) as IList;
            foreach (var o in arr)
                list.Add(o);
            object valObj = list;
            var clause = $"@{Context.Parameters.NextParameterName()}.Contains({property.Name})";
            var relationConverter = RelationConverters.FirstOrDefault(x => x.CanConvert(ClientTypes.EntityFramework,
                entityType, relations).ThrowIfCouldNotConvert());
            if (relationConverter == null)
                throw new Exception($"Converter '{relations}' for {ClientTypes.EntityFramework} not found.");
            clause = relationConverter.Convert(entityType, relations, Context.Parameters.CurrentIndex(), clause);
            Context.Parameters.Statement.Append(clause);
            Context.Parameters.Values.Add(valObj);
        }
    }
}