using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FeiniuBus
{
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

        private DynamicQueryParam CreateParam(QueryOperation opt, string field, object value)
        {
            return new DynamicQueryParam { Operator = opt, Field = field, Value = value };
        }

        public DynamicQueryParamBuilder Equal(string field, object value)
        {
            return Add(CreateParam(QueryOperation.Equal, field, value));
        }

        public DynamicQueryParamBuilder LessThan(string field, object value)
        {
            return Add(CreateParam(QueryOperation.LessThan, field, value));
        }

        public DynamicQueryParamBuilder LessThanOrEqual(string field, object value)
        {
            return Add(CreateParam(QueryOperation.LessThanOrEqual, field, value));
        }

        public DynamicQueryParamBuilder GreaterThan(string field, object value)
        {
            return Add(CreateParam(QueryOperation.GreaterThan, field, value));
        }

        public DynamicQueryParamBuilder GreaterThanOrEqual(string field, object value)
        {
            return Add(CreateParam(QueryOperation.GreaterThanOrEqual, field, value));
        }

        public DynamicQueryParamBuilder Contains(string field, object value)
        {
            return Add(CreateParam(QueryOperation.Contains, field, value));
        }

        public DynamicQueryParamBuilder StartsWith(string field, object value)
        {
            return Add(CreateParam(QueryOperation.StartsWith, field, value));
        }

        public DynamicQueryParamBuilder EndsWith(string field, object value)
        {
            return Add(CreateParam(QueryOperation.EndsWith, field, value));
        }

        public DynamicQueryParamBuilder In(string field, object value)
        {
            return Add(CreateParam(QueryOperation.In, field, value));
        }


        public DynamicQueryParamBuilder Any(string field, Action<DynamicQueryParamGroupBuilder> builder)
        {
            var group = new DynamicQueryParamGroup();
            var bu = new DynamicQueryParamGroupBuilder(group);
            builder.Invoke(bu);
            return Add(CreateParam(QueryOperation.Any, field, JsonConvert.SerializeObject(group)));
        }

        public DynamicQueryParamBuilder DataTimeLessThanOrEqualThenDay(string field, DateTime value)
        {
            return Add(CreateParam(QueryOperation.DataTimeLessThanOrEqualThenDay, field, value));
        }
    }
}