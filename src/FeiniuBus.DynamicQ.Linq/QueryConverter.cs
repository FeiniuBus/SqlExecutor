using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace FeiniuBus.DynamicQ.Linq
{
    public class LinqQueryConverter : IQueryConverter<LinqConvertResult>
    {
        private readonly LinqContext _context;
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _applicationServices;

        public LinqQueryConverter(IServiceProvider applicationServices)
        {
            _scope = applicationServices.CreateScope();
            _applicationServices = _scope.ServiceProvider;
            _context = _applicationServices.GetRequiredService<LinqContext>();
        }

        public Convertable CanConvert(ClientTypes clientType, DynamicQuery query, Type entityType, bool characterConverter)
        {
            return clientType == ClientTypes.EntityFramework;
        }

        public LinqConvertResult Convert(ClientTypes clientType, DynamicQuery query, Type entityType, bool characterConverter)
        {
            var groupConverters = _applicationServices.GetServices<IParameterGroupConverter>();
            var groupConverter = groupConverters.FirstOrDefault(x =>
                x.CanConvert(ClientTypes.EntityFramework, entityType, query.ParamGroup, query.ParamGroup.Relation).ThrowIfCouldNotConvert());
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
                Where = _context.Parameters.Statement.ToString().Replace("( && (", "((").Replace("( || (", "(("),
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

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}