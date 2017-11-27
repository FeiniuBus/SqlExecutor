using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;

namespace FeiniuBus.LinqBuilder
{
    internal static class ConverterHelper
    {
        private static readonly IDictionary<string, PropertyInfo[]> PropertyArrCache =
            new Dictionary<string, PropertyInfo[]>();

        public static string ConverterSelect(string select)
        {
            if (string.IsNullOrWhiteSpace(select))
                return "";
            if (!select.StartsWith("new("))
                return $"new({select})";
            return select;
        }

        public static object ChangeType(object objValue, Type conversionType)
        {
            object value = null;
            if (!conversionType.GetTypeInfo().IsGenericType)
            {
                value = conversionType.Name.Equals("Guid")
                    ? Guid.Parse(objValue.ToString())
                    : (conversionType.GetTypeInfo().IsEnum
                        ? Enum.Parse(conversionType, objValue.ToString())
                        : Convert.ChangeType(objValue, conversionType));
                //;
            }
            else
            {
                var genericTypeDefinition = conversionType.GetGenericTypeDefinition();
                if (genericTypeDefinition == typeof(Nullable<>))
                {
                    var converter = new NullableConverter(conversionType);
                    value = converter.ConvertFromString(objValue.ToString());
                }
            }
            return value;
        }

        public static void CheckQueryParamGroup([NotNull] DynamicQueryParamGroup group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            if (group.ChildGroups != null && group.ChildGroups.Any() && group.Params != null && group.Params.Any())
                throw new Exception("QueryParamGroup中不能同时存在Groups和Params");
        }

        public static bool IsParam([NotNull] DynamicQueryParamGroup group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            return group.Params != null && group.Params.Any();
        }

        public static PropertyInfo[] GetPropertyArr(Type entityType, string field)
        {
            lock (PropertyArrCache)
            {
                var key = $"{entityType.FullName}_{field}";
                if (PropertyArrCache.ContainsKey(key))
                    return PropertyArrCache[key];
                //获取每级属性如c.Users.Proiles.UserId
                var resArr = new List<PropertyInfo>();
                var props = field.Split('.');
                var typeOfProp = entityType;
                var i = 0;
                do
                {
                    var property = typeOfProp.GetProperty(props[i],
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (property == null)
                        throw new ArgumentException(field);
                    resArr.Add(property);
                    typeOfProp = property.PropertyType;
                    i++;
                } while (i < props.Length);
                var res = resArr.ToArray();
                PropertyArrCache.Add(key, res);
                return res;
            }
        }


        public static void ConverterQueryParamGroup(Type entityType, DynamicQueryParamGroup group,
            DynamicQueryKeyValueCollection collection)
        {
            CheckQueryParamGroup(group);
            if (IsParam(group))
                ConverterQueryParams(entityType, group.Params, collection, group.Relation);
            else
                ConverterQueryParamGroups(entityType, group.ChildGroups, collection, group.Relation);
        }

        public static void ConverterQueryParamGroups([NotNull] Type entityType,
            [NotNull] List<DynamicQueryParamGroup> groups, [NotNull] DynamicQueryKeyValueCollection collection,
            QueryRelation relation)
        {
            if (!groups.Any()) return;
            var list = groups.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                CheckQueryParamGroup(item);
                if (item.Relation == QueryRelation.AndNot || item.Relation == QueryRelation.OrNot)
                    collection.Builder.Append("(");
                if (IsParam(item))
                {
                    ConverterQueryParams(entityType, item.Params, collection, item.Relation);
                }
                else
                {
                    collection.Builder.Append("(");
                    ConverterQueryParamGroups(entityType, item.ChildGroups, collection, item.Relation);
                    collection.Builder.Append(")");
                }
                if (item.Relation == QueryRelation.AndNot || item.Relation == QueryRelation.OrNot)
                    collection.Builder.Append(" == false )");
                if (i < list.Count - 1)
                    collection.Builder.Append(relation == QueryRelation.Or || relation == QueryRelation.OrNot
                        ? " || "
                        : " && ");
            }
        }

        public static void ConverterQueryParams(Type entityType, [NotNull] List<DynamicQueryParam> queryParams,
            DynamicQueryKeyValueCollection collection, QueryRelation relation)
        {
            if (!queryParams.Any())
                return;
            var list = queryParams.ToList();
            collection.Builder.Append("(");
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var propertyType = GetPropertyArr(entityType, item.Field).Last();
                var provider = MethodProviderCollection.Get(item, propertyType.PropertyType);
                provider.Converter(item, propertyType.PropertyType, collection);
                if (i < list.Count - 1)
                    collection.Builder.Append(relation == QueryRelation.Or ? " || " : " && ");
            }
            collection.Builder.Append(")");
        }

        public static string ConverterOrderBy([NotNull] List<DynamicQueryOrder> orderCollection)
        {
            if (!orderCollection.Any())
                return string.Empty;
            var list = orderCollection.ToList();

            var res = new StringBuilder();

            for (var i = 0; i < list.Count(); i++)
            {
                var item = list[i];
                var order = item.Sort == ListSortDirection.Ascending
                    ? "ascending"
                    : "descending";
                res.Append($"{item.Name} {order}");
                if (i < list.Count - 1)
                    res.Append(',');
            }

            return res.ToString();
        }
    }
}