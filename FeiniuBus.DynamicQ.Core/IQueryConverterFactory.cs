using System;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ
{
    public interface IQueryConverterFactory
    {
        IQueryConverter<TResult> Create<TResult>(ClientTypes clientType, DynamicQuery query, Type entityType,
            bool characterConverter = false);
    }
}