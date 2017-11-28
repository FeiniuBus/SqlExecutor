using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using FeiniuBus.DynamicQ.Infrastructure;
using Newtonsoft.Json;

namespace FeiniuBus.DynamicQ.Builder
{
    public class DynamicQueryBuilder
    {
        private readonly DynamicQuery _query;

        public DynamicQueryBuilder() : this(true)
        {
        }

        public DynamicQueryBuilder(bool pager)
        {
            _query = new DynamicQuery(pager);
            ParamGroupBuilder = new DynamicQueryParamGroupBuilder(_query.ParamGroup);
        }

        public DynamicQueryParamGroupBuilder ParamGroupBuilder { get; }

        public DynamicQueryBuilder Skip(int skip)
        {
            _query.Skip = skip;
            return this;
        }

        public DynamicQueryBuilder Take(int take)
        {
            _query.Take = take;
            return this;
        }

        public DynamicQueryBuilder Pager(bool pager)
        {
            _query.Pager = pager;
            return this;
        }

        public DynamicQueryBuilder Select(string select)
        {
            _query.Select = select;
            return this;
        }

        public DynamicQueryBuilder OrderBy(string name, ListSortDirection sort = ListSortDirection.Ascending)
        {
            if (!_query.Order.Any(i => i.Name == name && i.Sort == sort))
                _query.Order.Add(new DynamicQueryOrder {Name = name, Sort = sort});
            return this;
        }

        public DynamicQuery Build()
        {
            return _query;
        }

        public static DynamicQueryBuilder Create(bool pager = true)
        {
            return new DynamicQueryBuilder(pager);
        }
    }

    public class DynamicQueryParamBuilder
    {
        private readonly List<DynamicQueryParam> _params;

        public DynamicQueryParamBuilder() : this(new List<DynamicQueryParam>())
        {
        }

        public DynamicQueryParamBuilder(List<DynamicQueryParam> queryParams)
        {
            _params = queryParams;
        }


        private DynamicQueryParamBuilder Add(DynamicQueryParam param)
        {
            _params.Add(param);
            return this;
        }

        private DynamicQueryParam CreateParam(QueryOperations opt, string field, object value)
        {
            return new DynamicQueryParam {Operator = opt, Field = field, Value = value};
        }

        public DynamicQueryParamBuilder Equal(string field, object value)
        {
            return Add(CreateParam(QueryOperations.Equal, field, value));
        }

        public DynamicQueryParamBuilder LessThan(string field, object value)
        {
            return Add(CreateParam(QueryOperations.LessThan, field, value));
        }

        public DynamicQueryParamBuilder LessThanOrEqual(string field, object value)
        {
            return Add(CreateParam(QueryOperations.LessThanOrEqual, field, value));
        }

        public DynamicQueryParamBuilder GreaterThan(string field, object value)
        {
            return Add(CreateParam(QueryOperations.GreaterThan, field, value));
        }

        public DynamicQueryParamBuilder GreaterThanOrEqual(string field, object value)
        {
            return Add(CreateParam(QueryOperations.GreaterThanOrEqual, field, value));
        }

        public DynamicQueryParamBuilder Contains(string field, object value)
        {
            return Add(CreateParam(QueryOperations.Contains, field, value));
        }

        public DynamicQueryParamBuilder StartsWith(string field, object value)
        {
            return Add(CreateParam(QueryOperations.StartsWith, field, value));
        }

        public DynamicQueryParamBuilder EndsWith(string field, object value)
        {
            return Add(CreateParam(QueryOperations.EndsWith, field, value));
        }

        public DynamicQueryParamBuilder In(string field, object value)
        {
            return Add(CreateParam(QueryOperations.In, field, value));
        }


        public DynamicQueryParamBuilder Any(string field, Action<DynamicQueryParamGroupBuilder> builder)
        {
            var group = new DynamicQueryParamGroup();
            var bu = new DynamicQueryParamGroupBuilder(group);
            builder.Invoke(bu);
            return Add(CreateParam(QueryOperations.Any, field, JsonConvert.SerializeObject(group)));
        }

        //public DynamicQueryParamBuilder DataTimeLessThanOrEqualThenDay(string field, DateTime value)
        //{
        //    return Add(CreateParam(QueryOperations.DataTimeLessThanOrEqualThenDay, field, value));
        //}
    }

    public class DynamicQueryParamGroupBuilder
    {
        private readonly DynamicQueryParamGroup _paramGroup;

        public DynamicQueryParamGroupBuilder() : this(new DynamicQueryParamGroup())
        {
        }

        public DynamicQueryParamGroupBuilder(DynamicQueryParamGroup paramGroup)
        {
            _paramGroup = paramGroup;
            ParamBuilder = new DynamicQueryParamBuilder(_paramGroup.Params);
        }

        public DynamicQueryParamBuilder ParamBuilder { get; }

        public DynamicQueryParamGroupBuilder RelationAnd()
        {
            return Relation(QueryRelations.And);
        }

        public DynamicQueryParamGroupBuilder RelationOr()
        {
            return Relation(QueryRelations.Or);
        }

        public DynamicQueryParamGroupBuilder Relation(QueryRelations relation)
        {
            _paramGroup.Relation = relation;
            return this;
        }

        public DynamicQueryParamGroup Build()
        {
            return _paramGroup;
        }

        #region CreateChildGroup

        public DynamicQueryParamGroupBuilder CreateChildGroup(QueryRelations relation)
        {
            _paramGroup.Check();
            var childGroup = new DynamicQueryParamGroup {Relation = relation};
            _paramGroup.ChildGroups.Add(childGroup);
            return new DynamicQueryParamGroupBuilder(childGroup);
        }

        public DynamicQueryParamGroupBuilder CreateChildAndGroup()
        {
            return CreateChildGroup(QueryRelations.And);
        }

        public DynamicQueryParamGroupBuilder CreateChildOrGroup()
        {
            return CreateChildGroup(QueryRelations.Or);
        }

        public DynamicQueryParamGroupBuilder CreateChildAndNotGroup()
        {
            return CreateChildGroup(QueryRelations.AndNot);
        }

        public DynamicQueryParamGroupBuilder CreateChildOrNotGroup()
        {
            return CreateChildGroup(QueryRelations.OrNot);
        }

        #endregion
    }
}