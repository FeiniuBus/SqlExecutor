using System.Collections;
using System.Reflection;
using System.Threading;

namespace FeiniuBus.DynamicQ.Internal
{
    public sealed class PropertyAccessor : IPropertyAccessor
    {
        private readonly Hashtable _source;
        private static readonly object LockObj = new object();

        public PropertyAccessor()
        {
            _source = new Hashtable();
        }


        public void Store(string key, PropertyInfo propertyInfo)
        {
            Monitor.Enter(LockObj);
            _source[key] = propertyInfo;
            Monitor.Exit(LockObj);
        }

        public PropertyInfo Load(string key)
        {
            return _source.ContainsKey(key) ? _source[key] as PropertyInfo : null;
        }
    }
}
