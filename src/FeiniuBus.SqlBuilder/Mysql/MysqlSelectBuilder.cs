using System;
using System.Collections.Generic;
using System.Linq;
using FeiniuBus.Converter;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlSelectBuilder : MysqlWhereBuilder, ISelectBuilder
    {
        public MysqlSelectBuilder(IServiceProvider serviceProvider) : base(serviceProvider
            .GetRequiredService<ICharacterConverter>())
        {
            ServiceProvider = serviceProvider;
        }

        internal IServiceProvider ServiceProvider { get; set; }
        internal IServiceScope ServiceScope { get; set; }

        [Obsolete("Unsupported.")]
        public SqlBuiderResult Build()
        {
            throw new NotImplementedException("暂时不支持SelectBuilder.");
        }

        public ISelectBuilder CreateScope()
        {
            var scope = ServiceProvider.CreateScope();
            var builder = scope.ServiceProvider.GetRequiredService<ISelectBuilder>();

            if (builder is MysqlSelectBuilder)
                ((MysqlSelectBuilder) builder).ServiceScope = scope;

            return builder;
        }

        public void Dispose()
        {
            try
            {
                ServiceScope?.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        public string OrderBy(IEnumerable<DynamicQueryOrder> orders, bool fieldConverter = true)
        {
            var dynamicQueryOrders = orders as IList<DynamicQueryOrder> ?? orders.ToList();
            if (orders == null || !dynamicQueryOrders.Any()) return string.Empty;

            return string.Join(",", dynamicQueryOrders.Select(item =>
            {
                var name = item.Name;

                if (fieldConverter)
                    name = CharacterConverter.FieldConverter(item.Name);

                var name1 = name;
                if (SqlFieldMappings.Any(x => x.Key == name1))
                {
                    var name2 = name;
                    name = SqlFieldMappings.First(x => x.Key == name2).SqlField;
                }

                var order = item.Sort == ListSortDirection.Ascending
                    ? "ASC"
                    : "DESC";

                return $" {name} {order} ";
            }));
        }
    }
}