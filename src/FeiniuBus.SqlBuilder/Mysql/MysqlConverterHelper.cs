using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace FeiniuBus.SqlBuilder.Mysql
{
    internal class MysqlConverterHelper
    {
        private static readonly string __PAM_ = "PARAM_";

        private string GetSqlKey(SqlFieldMappings mappings, DynamicQueryParam param, string parentKey)
        {
            var file = param.Field;
            if (!string.IsNullOrWhiteSpace(parentKey))
                file = $"{parentKey}.{param.Field}";

            var item = mappings.SingleOrDefault(i => i.Key == file);
            if (item != null)
                return item.SqlField;
            return file;
        }


        private object GetSqlValue(SqlFieldMappings mappings, DynamicQueryParam param, string parentKey)
        {
            var file = param.Field;
            if (!string.IsNullOrWhiteSpace(parentKey))
                file = $"{parentKey}.{param.Field}";

            var item = mappings.SingleOrDefault(i => i.Key == file);
            if (item?.Type != null && item.Type.GetTypeInfo().IsEnum)
            {
                if (param.Operator == QueryOperation.In)
                {
                    var arr = param.Value.ToString().Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);
                    var objArr = new List<string>();
                    foreach (var s in arr)
                    {
                        var enu = Convert.ChangeType(Enum.Parse(item.Type, s), typeof(int));
                        objArr.Add(enu.ToString());
                    }
                    return string.Join(",", objArr);
                }
                return Convert.ChangeType(Enum.Parse(item.Type, param.Value.ToString()), typeof(int));
            }
            return param.Value;
        }

        private bool IsParam(DynamicQueryParamGroup group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            return group.Params != null && group.Params.Any();
        }

        public void CheckQueryParamGroup(DynamicQueryParamGroup group)
        {
            if (group == null) throw new ArgumentNullException(nameof(group));
            if (group.ChildGroups != null && group.ChildGroups.Any() && group.Params != null && group.Params.Any())
                throw new Exception("QueryParamGroup中不能同时存在Groups和Params");
        }

        public void ConverterQueryParamGroup(DynamicQueryParamGroup group, SqlBuiderResult collection,
            SqlFieldMappings mappings, string
                parentKey)
        {
            CheckQueryParamGroup(group);
            if (IsParam(group))
                ConverterQueryParams(group.Params, collection, group.Relation, mappings, parentKey);
            else
                ConverterQueryParamGroups(group.ChildGroups, collection, group.Relation, mappings, parentKey);
        }


        public void ConverterQueryParamGroups(List<DynamicQueryParamGroup> groups, SqlBuiderResult collection,
            QueryRelation relation, SqlFieldMappings mappings, string parentKey)
        {
            if (!groups.Any()) return;
            var list = groups.ToList();
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                CheckQueryParamGroup(item);
                if (item.Relation == QueryRelation.AndNot || item.Relation == QueryRelation.OrNot)
                    collection.SqlString.Append("(");
                if (IsParam(item))
                {
                    ConverterQueryParams(item.Params, collection, item.Relation, mappings, parentKey);
                }
                else
                {
                    collection.SqlString.Append("(");
                    ConverterQueryParamGroups(item.ChildGroups, collection, item.Relation, mappings, parentKey);
                    collection.SqlString.Append(")");
                }
                if (item.Relation == QueryRelation.AndNot || item.Relation == QueryRelation.OrNot)
                    collection.SqlString.Append("=false)");
                if (i < list.Count - 1)
                    collection.SqlString.Append(relation == QueryRelation.And || relation == QueryRelation.AndNot
                        ? " AND "
                        : " OR ");
            }
        }


        public void ConverterQueryParams(List<DynamicQueryParam> queryParams, SqlBuiderResult collection,
            QueryRelation relation, SqlFieldMappings mappings, string parentKey)
        {
            if (!queryParams.Any())
                return;
            var list = queryParams.ToList();
            collection.SqlString.Append("(");
            for (var i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var field = item.Operator == QueryOperation.Any ? "" : GetSqlKey(mappings, item, parentKey);
                var param = $"@{__PAM_}{collection.Params.Count}";
                var val = GetSqlValue(mappings, item, parentKey);

                switch (item.Operator)
                {
                    case QueryOperation.Equal:
                        collection.SqlString.Append($"{field} = {param}");
                        collection.Params.Add(param, val);
                        break;
                    case QueryOperation.LessThan:
                        collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} < {param}");
                        break;
                    case QueryOperation.LessThanOrEqual:
                        collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} <= {param}");
                        break;
                    case QueryOperation.GreaterThan:
                        collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} > {param}");
                        break;
                    case QueryOperation.GreaterThanOrEqual:
                        collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} >= {param}");
                        break;
                    case QueryOperation.Contains:
                        collection.Params.Add(param, $"%{val}%");
                        collection.SqlString.Append($"{field} like {param}");
                        break;
                    case QueryOperation.StartsWith:
                        collection.Params.Add(param, $"{val}%");
                        collection.SqlString.Append($"{field} like {param}");
                        break;
                    case QueryOperation.EndsWith:
                        collection.Params.Add(param, $"%{val}");
                        collection.SqlString.Append($"{field} like {param}");
                        break;
                    case QueryOperation.DataTimeLessThanOrEqualThenDay:
                        if (DateTime.TryParse(val.ToString(), out var date))
                            val = date.AddDays(1).AddMilliseconds(-1);
                        else
                            break;
                        collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} <= {param}");
                        break;
                    case QueryOperation.In:
                        //collection.Params.Add(param, val);
                        collection.SqlString.Append($"{field} IN ({val.ToString().Trim(',')})");
                        break;
                    case QueryOperation.Any:
                        var group = JsonConvert.DeserializeObject<DynamicQueryParamGroup>((val ?? "").ToString());
                        if (group == null || group.ChildGroups == null)
                            break;
                        ConverterQueryParamGroup(group, collection, mappings, item.Field);
                        break;
                    default:
                        throw new ArgumentException($"{nameof(QueryOperation)}无效");
                }


                if (i < list.Count - 1)
                    collection.SqlString.Append(relation == QueryRelation.Or ? " OR " : " AND ");
            }
            collection.SqlString.Append(")");
        }
    }
}