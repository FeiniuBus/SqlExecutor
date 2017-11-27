using System.Collections.Generic;

namespace FeiniuBus
{
    public interface IHashObject : IDictionary<string, object>
    {
        bool CheckSetValue(string key, object value);
        T GetValue<T>(string key);
        T GetValue<T>(string key, T defaultValue);
    }
}