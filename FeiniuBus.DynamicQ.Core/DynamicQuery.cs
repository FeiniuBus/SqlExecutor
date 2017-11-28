using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FeiniuBus.DynamicQ.Infrastructure;
using FeiniuBus.DynamicQ.Internal;

namespace FeiniuBus.DynamicQ
{
    public class DynamicQuery
    {
        public DynamicQuery() : this(true)
        {
        }

        public DynamicQuery(bool pager)
        {
            ParamGroup = new DynamicQueryParamGroup();
            Order = new List<DynamicQueryOrder>();
            Pager = pager;
        }

        public DynamicQueryParamGroup ParamGroup { get; set; }
        public List<DynamicQueryOrder> Order { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 10;
        public bool Pager { get; set; }
        public string Select { get; set; }
    }

    public class DynamicQueryParamGroup
    {
        public DynamicQueryParamGroup()
        {
            ChildGroups = new List<DynamicQueryParamGroup>();
            Params = new List<DynamicQueryParam>();
        }

        public QueryRelations Relation { get; set; }
        public List<DynamicQueryParamGroup> ChildGroups { get; }
        public List<DynamicQueryParam> Params { get; }

        public void Check()
        {
            if (Params.Any() && ChildGroups.Any())
                throw new Exception("DynamicQueryParamGroup不能同时添加Params和Group");
        }


        public bool IsParam()
        {
            return Params != null && Params.Any();
        }
    }

    public class DynamicQueryParam
    {
        public string Field { get; set; }
        public QueryOperations Operator { get; set; }
        public object Value { get; set; }

        public PropertyInfo GetPropertyInfo(Type entityType, IPropertyAccessor propertyAccessor)
        {
            var key = $"{entityType.FullName}_{Field}";
            var property = propertyAccessor.Load(key);
            var typeOfProp = entityType;
            if (property == null)
            {
                var props = Field.Split('.');
                var k = "";
                for (var i = 0; i < props.Length; i++)
                {
                    property = typeOfProp.GetProperty(props[i],
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    if (property == null)
                        throw new ArgumentException($"Field {Field} not found in {entityType.FullName}");
                    k += (i>0 ? "." : "") + props[i];
                    propertyAccessor.Store($"{entityType.FullName}_{k}", property);
                }
            }
            return property;
        }
    }

    public class DynamicQueryOrder
    {
        public string Name { get; set; }
        public ListSortDirection Sort { get; set; }
    }
}