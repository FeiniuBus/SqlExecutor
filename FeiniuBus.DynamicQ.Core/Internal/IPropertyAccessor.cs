using System.Reflection;

namespace FeiniuBus.DynamicQ.Internal
{
    public interface IPropertyAccessor
    {
        void Store(string key, PropertyInfo propertyInfo);

        PropertyInfo Load(string key);
    }
}