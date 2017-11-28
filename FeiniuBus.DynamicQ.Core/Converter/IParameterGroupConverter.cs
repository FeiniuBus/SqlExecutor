using System;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Converter
{
    public interface IParameterGroupConverter
    {
        Convertable CanConvert(ClientTypes clientType, Type entityType, DynamicQueryParamGroup group, QueryRelations relation);

        void Convert(Type entityType, DynamicQueryParamGroup group, QueryRelations relation);
    }
}