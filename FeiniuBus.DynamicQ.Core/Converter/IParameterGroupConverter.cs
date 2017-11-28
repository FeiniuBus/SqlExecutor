using System;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Converter
{
    public interface IParameterGroupConverter
    {
        bool CanConvert(ClientTypes clientType, Type entityType, DynamicQueryParamGroup group, QueryRelations relation);

        void Convert(Type entityType, DynamicQueryParamGroup group, QueryRelations relation);
    }
}