using System;
using System.ComponentModel;
using System.Reflection;

namespace FeiniuBus.DynamicQ.Linq
{
    internal static class InternalExtensions
    {
        internal static object ChangeType(this object obj, Type conversionType)
        {
            object value = null;
            if (!conversionType.GetTypeInfo().IsGenericType)
            {
                value = conversionType.Name.Equals("Guid")
                    ? Guid.Parse(obj.ToString())
                    : (conversionType.GetTypeInfo().IsEnum
                        ? Enum.Parse(conversionType, obj.ToString())
                        : Convert.ChangeType(obj, conversionType));
            }
            else
            {
                var genericTypeDefinition = conversionType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    var converter = new NullableConverter(conversionType);
                    value = converter.ConvertFromString(obj.ToString());
                }
            }
            return value;
        }
    }
}