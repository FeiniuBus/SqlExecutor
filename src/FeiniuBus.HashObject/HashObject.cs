using System;
using System.Collections.Generic;
using System.Reflection;

namespace FeiniuBus
{
    public class HashObject : Dictionary<string, object>, IHashObject
    {
        public HashObject()
        {
        }
        public HashObject(IDictionary<string, object> dictionary) : base(dictionary)
        {
        }
        public bool CheckSetValue(string key, object value)
        {
            if (ContainsKey(key))
            {
                return false;
            }
            this[key] = value;
            return true;
        }
        public T GetValue<T>(string key, T defaultValue)
        {
            object obj2;
            if (TryGetValue(key, out obj2))
            {
                var type = typeof(T);
                var underlyingType = type;
                if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    var t = Nullable.GetUnderlyingType(type);
                    if (obj2 == null)
                    {
                        return defaultValue;
                    }
                    return (T)ChangeType(obj2, t);
                }
                if (obj2 != null && IsPrimitiveType(type, out underlyingType))
                {
                    return (T)ChangeType(obj2, underlyingType);
                }
                if (!(obj2 == null || underlyingType == type || !underlyingType.GetTypeInfo().IsEnum || obj2.GetType().GetTypeInfo().IsEnum))
                {
                    return (T)Enum.ToObject(underlyingType, obj2);
                }

                if (obj2 == null && underlyingType.GetTypeInfo().IsSubclassOf(typeof(ValueType)))
                {
                    return defaultValue;
                }

                return (T)obj2;

            }
            return defaultValue;
        }
        private bool IsPrimitiveType(Type type)
        {
            return type.GetTypeInfo().IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(decimal);
        }
        private bool IsPrimitiveType(Type type, out Type underlyingType)
        {
            underlyingType = type;
            if (IsPrimitiveType(type))
            {
                return true;
            }

            if (type.GetTypeInfo().IsValueType && type.GetTypeInfo().IsGenericType)
            {
                underlyingType = Nullable.GetUnderlyingType(type);
                return IsPrimitiveType(underlyingType);
            }

            return false;
        }
        internal static object ChangeType(object value, Type type)
        {
            return Convert.ChangeType(value, type, null);
        }
        public new object this[string key]
        {
            get
            {
                object obj2;
                try
                {
                    obj2 = base[key];
                }
                catch (KeyNotFoundException ex)
                {
                    throw new KeyNotFoundException(string.Format("关键字{0}不在HashObject中", key), ex);
                }

                return obj2;
            }
            set { base[key] = value; }
        }
        public T GetValue<T>(string key)
        {
            return GetValue(key, default(T));
        }
    }
}
