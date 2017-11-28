using FeiniuBus.DynamicQ.Converter;
using System.Collections.Generic;

namespace FeiniuBus.DynamicQ.Linq.Converters
{
    public abstract class ParamGroupConverterInjectedOperationConverter : BaseOperationConverter
    {
        protected ParamGroupConverterInjectedOperationConverter(LinqContext context, IEnumerable<IRelationConverter> relationConverters) : base(context, relationConverters)
        {
        }

        protected internal IParameterGroupConverter ParameterGroupConverter { get; internal set; }
    }
}
