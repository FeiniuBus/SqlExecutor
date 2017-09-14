using System;
using JetBrains.Annotations;

namespace FeiniuBus.LinqBuilder
{
    internal interface IMethodConverterProvider
    {
        bool Match([NotNull] DynamicQueryParam queryParam, [NotNull] Type propertyType);

        void Converter([NotNull] DynamicQueryParam queryParam, [NotNull] Type propertyType,
            [NotNull] DynamicQueryKeyValueCollection collection);
    }
}