using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;
using FeiniuBus.DynamicQ.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.DynamicQ.Linq
{
    public class LinqQueryConverter : IQueryConverter<LinqConvertResult>
    {
        private readonly LinqContext _context;
        private readonly IServiceScope _scope;

        public LinqQueryConverter(IServiceProvider applicationServices)
        {
            _scope = applicationServices.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<LinqContext>();
        }

        public bool CanConvert(ClientTypes clientType, DynamicQuery query, Type entityType, bool characterConverter)
        {
            return clientType == ClientTypes.EntityFramework;
        }

        public LinqConvertResult Convert(ClientTypes clientType, DynamicQuery query, Type entityType,
            bool characterConverter)
        {
            var applicationServices = _scope.ServiceProvider;
            var groupConverters = applicationServices.GetServices<IParameterGroupConverter>();
            var groupConverter = groupConverters.FirstOrDefault(x =>
                x.CanConvert(ClientTypes.EntityFramework, entityType, query.ParamGroup, query.ParamGroup.Relation));
            if (groupConverter == null)
                throw new Exception($"Converter of ParamGroup for {ClientTypes.EntityFramework} not found.");
            groupConverter.Convert(entityType, query.ParamGroup, query.ParamGroup.Relation);
            return new LinqConvertResult
            {
                IsPager = query.Pager,
                Order = ConvertOrderBy(query.Order),
                Select = ConvertSelect(query.Select),
                Skip = query.Skip,
                Take = query.Take,
                Where = _context.Parameters.Statement.ToString().Replace("( && (","(").Replace("( || (", "("),
                WhereValue = _context.Parameters.Values.ToArray()
            };
        }

        public string ConvertOrderBy(List<DynamicQueryOrder> orderCollection)
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

        public string ConvertSelect(string select)
        {
            if (string.IsNullOrWhiteSpace(select))
                return "";
            if (!select.StartsWith("new("))
                return $"new({select})";
            return select;
        }

        private void ConfigureReflection(Type entityType)
        {
            var accessor = _scope.ServiceProvider.GetRequiredService<IPropertyAccessor>();
            if (accessor.Load(entityType.FullName) != null) return;

        }
    }
}