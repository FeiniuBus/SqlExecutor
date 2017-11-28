using System;
using System.Collections.Generic;
using System.Linq;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;
using FeiniuBus.DynamicQ.Internal;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public sealed class ParameterGroupConverter : IParameterGroupConverter
    {
        private readonly LinqContext _context;
        private readonly IEnumerable<IOperationConverter> _operationConverters;
        private readonly IPropertyAccessor _propertyAccessor;
        private readonly IEnumerable<IRelationConverter> _relationConverters;

        public ParameterGroupConverter(LinqContext context, IEnumerable<IOperationConverter> operationConverters,
            IEnumerable<IRelationConverter> relationConverters, IPropertyAccessor propertyAccessor)
        {
            _context = context;
            _operationConverters = operationConverters;
            _relationConverters = relationConverters;
            _propertyAccessor = propertyAccessor;
        }

        public bool CanConvert(ClientTypes clientType, Type entityType, DynamicQueryParamGroup group,
            QueryRelations relation)
        {
            if (clientType != ClientTypes.EntityFramework) return false;
            group.Check();
            return true;
        }

        public void Convert(Type entityType, DynamicQueryParamGroup group, QueryRelations relation)
        {
            Recur(entityType, group, relation);
        }

        public void Recur(Type entityType, DynamicQueryParamGroup group, QueryRelations relation)
        {
            if (group.IsParam())
            {
                var relationConverter = _relationConverters.FirstOrDefault(x =>
                    x.CanConvert(ClientTypes.EntityFramework, entityType, relation));
                if (relationConverter == null)
                    throw new Exception(
                        $"Converter of '{relation}' for {ClientTypes.EntityFramework} not found.");

                _context.Parameters.Statement.Append(relationConverter.GetPrefix(entityType, relation,
                    _context.Parameters.CurrentIndex()));

                foreach (var p in group.Params)
                {
                    var converter = _operationConverters.FirstOrDefault(x =>
                        x.CanConvert(ClientTypes.EntityFramework, entityType, p.Field, p.Value, p.Operator,
                            p.GetPropertyInfo(entityType, _propertyAccessor), group.Relation));
                    if (converter == null)
                        throw new Exception(
                            $"Converter of '{p.Operator}' for {ClientTypes.EntityFramework} not found.");

                    converter.Convert(entityType, _context.Parameters.NextParameterName(), p.Field, p.Value, p.Operator,
                        p.GetPropertyInfo(entityType, _propertyAccessor), group.Relation);
                }

                _context.Parameters.Statement.Append(relationConverter.GetSuffix(entityType, relation,
                    _context.Parameters.CurrentIndex()));
            }
            else
            {
                foreach (var child in group.ChildGroups)
                    Recur(entityType, child, group.Relation);
            }
        }
    }
}