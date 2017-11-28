using System;

namespace FeiniuBus.Converter
{
    public interface IQueryConverter<out TResult>
    {
        bool CanConvert(ClientTypes clientType, DynamicQuery queryModel, Type entityType, bool characterConverter);

        TResult Convert(ClientTypes clientType, DynamicQuery queryModel, Type entityType, bool characterConverter);
    }
}
