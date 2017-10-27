using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MysqlSelectBuilder : MysqlWhereBuilder, ISelectBuilder
    {

        public MysqlSelectBuilder(IServiceProvider serviceProvider) : base(serviceProvider.GetRequiredService<ICharacterConverter>())
        {
            ServiceProvider = serviceProvider;
        }

        internal IServiceProvider ServiceProvider { get; set; }
        internal IServiceScope ServiceScope { get; set; }

        [Obsolete("Unsupported.")]
        public SqlBuiderResult Build()
        {
            throw new System.NotImplementedException("暂时不支持SelectBuilder.");
        }

        public ISelectBuilder CreateScope()
        {
            var scope = ServiceProvider.CreateScope();
            var builder = scope.ServiceProvider.GetRequiredService<ISelectBuilder>();
            
            if(builder is MysqlSelectBuilder)
            {
                ((MysqlSelectBuilder)builder).ServiceScope = scope;
            }

            return builder;
        }

        public void Dispose()
        {
            try
            {
                ServiceScope?.Dispose();
            }
            catch { }
        }

        public string OrderBy(IEnumerable<DynamicQueryOrder> orders, bool fieldConverter = true)
        {
            if (orders == null || !orders.Any()) return string.Empty;

            return string.Join(",", orders.Select(item =>
            {
                var name = item.Name;

                if (fieldConverter)
                {
                    name = CharacterConverter.FieldConverter(item.Name);
                }

                if (SqlFieldMappings.Any(x=>x.Key == item.Name))
                {
                    name = SqlFieldMappings.First(x => x.Key == item.Name).SqlField;
                }
               
                var order = item.Sort == ListSortDirection.Ascending
                  ? "ASC"
                  : "DESC";
               
                return $" {name} {order} ";

            }));
        }
    }
}
