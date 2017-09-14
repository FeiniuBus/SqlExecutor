using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace FeiniuBus.LinqBuilder
{
    public static class DynamicQueryCharacterConverter
    {
        public static DynamicQuery Converter(DynamicQuery dynamicQuery)
        {
            if (dynamicQuery == null)
                return null;

            var result = new DynamicQuery(dynamicQuery.Pager);
            result.Take = dynamicQuery.Take;
            result.Skip = dynamicQuery.Skip;

            result.ParamGroup = new DynamicQueryParamGroup();
            result.Order = ConverterOrderFieldName(dynamicQuery.Order);
            if (dynamicQuery.ParamGroup != null)
            {
                var newgroup = new DynamicQueryParamGroup();
                ConverterDynamicQueryParamGroupFieldName(newgroup, dynamicQuery.ParamGroup);
                result.ParamGroup = newgroup;
            }
            result.Select = ConverterSelectFieldName(dynamicQuery.Select);
            return result;
        }

        public static List<DynamicQueryOrder> ConverterOrderFieldName(List<DynamicQueryOrder> orders)
        {
            var result = new List<DynamicQueryOrder>();
            if (orders != null)
            {
                foreach (var item in orders)
                {
                    result.Add(new DynamicQueryOrder() { Name = FieldConverter(item.Name), Sort = item.Sort });
                }
            }

            return result;
        }

        public static void ConverterDynamicQueryParamGroupFieldName(DynamicQueryParamGroup newGroup, DynamicQueryParamGroup old)
        {
            if (newGroup == null)
                throw new ArgumentNullException(nameof(newGroup));
            if (old == null)
                throw new ArgumentNullException(nameof(old));
            newGroup.Relation = old.Relation;
            if (old.Params != null && old.Params.Any())
            {
                foreach (var item in old.Params)
                {
                    var param = new DynamicQueryParam();
                    param.Operator = item.Operator;
                    param.Field = FieldConverter(item.Field);
                    //处理特殊情况any
                    if (item.Operator == QueryOperation.Any)
                    {
                        var anyGroup = JsonConvert.DeserializeObject<DynamicQueryParamGroup>(item.Value?.ToString() ?? "");
                        var newAnyGroup = new DynamicQueryParamGroup();
                        ConverterDynamicQueryParamGroupFieldName(newAnyGroup, anyGroup);
                        param.Value = JsonConvert.SerializeObject(newAnyGroup);
                    }
                    else
                    {
                        param.Value = item.Value;
                    }
                    newGroup.Params.Add(param);
                }
            }
            if (old.ChildGroups != null && old.ChildGroups.Any())
            {
               
                foreach (var childGroup in old.ChildGroups)
                {
                    var newChildGroup = new DynamicQueryParamGroup();
                    ConverterDynamicQueryParamGroupFieldName(newChildGroup, childGroup);
                    newGroup.ChildGroups.Add(newChildGroup);
                }
               
            }

        }

        public static string ConverterSelectFieldName(string select)
        {
            if (select == null) return null;
            return string.Join("", select.Split(',').Select(FieldConverter));
        }

        public static string FieldConverter(string fieldName)
        {
            var res = string.Empty;
            if (string.IsNullOrWhiteSpace(fieldName))
                return res;
            var arr = fieldName.Trim().Split('.').Where(i => !string.IsNullOrWhiteSpace(i)).ToList();

            for (int i = 0; i < arr.Count; i++)
            {
                var lineArr = arr[i].Split('_');
                foreach (var line in lineArr)
                {
                    if (line.Length > 1)
                        res += line.Substring(0, 1).ToUpper() + line.Substring(1);
                    else res += line.ToLower();
                }
                if (i < arr.Count - 1)
                    res += '.';
            }

            return res;
        }
    }
}
