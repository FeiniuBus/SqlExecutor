using System;
using FeiniuBus.DynamicQ.Infrastructure;

namespace FeiniuBus.DynamicQ.Converter
{
    public interface IQueryConverter<out TResult> : IDisposable
    {
        Convertable CanConvert(ClientTypes clientType, DynamicQuery query, Type entityType, bool characterConverter);

        TResult Convert(ClientTypes clientType, DynamicQuery query, Type entityType, bool characterConverter);
    }
}