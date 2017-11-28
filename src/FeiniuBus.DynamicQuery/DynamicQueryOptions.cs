using FeiniuBus.Converter;
using System.Collections.Generic;

namespace FeiniuBus
{
    public sealed class DynamicQueryOptions
    {
        public DynamicQueryOptions()
        {
            OperationConverters = new List<IOperationConverter>();
        }

        public IList<IOperationConverter> OperationConverters { get; }
    }
}
