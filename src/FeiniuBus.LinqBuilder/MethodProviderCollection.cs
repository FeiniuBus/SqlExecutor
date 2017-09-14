using System;
using System.Collections.Generic;
using System.Linq;

namespace FeiniuBus.LinqBuilder
{
    internal static class MethodProviderCollection
    {
        private static readonly List<IMethodConverterProvider> Collections;
        internal static readonly IMethodConverterProvider DefaultConverterProvider;

        static MethodProviderCollection()
        {
            Collections = new List<IMethodConverterProvider>
            {
                new MethodConverterByAny(),
                new MethodConverterByIn(),
                new MethodConverterByDataTimeLessThanOrEqualThenDay()
            };
            DefaultConverterProvider = new MethodConverter();
        }

        public static IMethodConverterProvider Get(DynamicQueryParam queryParam, Type propertyType)
        {
            var res = Collections.FirstOrDefault(i => i.Match(queryParam, propertyType));
            if (res == null)
            {
                return DefaultConverterProvider;
            }
            return res;
        }
    }
}