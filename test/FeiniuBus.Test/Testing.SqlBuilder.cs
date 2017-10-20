using FeiniuBus.SqlBuilder;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
        public SqlFieldMappings BuildMappings()
        {
            var mappings = new SqlFieldMappings();
            mappings.Map("Extras.Guest", "t2.Guest")
                    .Map("Address", "t1.Address")
                    .Map("Disabled", "t1.Disabled")
                    .Map("Amout", "t1.Amout")
                    .Map("Price", "t1.Price")
                    .Map("Drink", "t1.Drink")
                    .Map("Count", "t1.Count")
                    .Map("Total", "t1.Total");
            // Native api also be supported
            mappings.Add("Url", "t1.Url");
            return mappings;
        }

        public string BuilderWhereClause(DynamicQuery dynamicQuery)
        {
            var selectBuilder = ServiceProvider.GetRequiredService<ISelectBuilder>();
            selectBuilder.Mapping(BuildMappings());
            selectBuilder.Where(dynamicQuery.ParamGroup);
            var result = selectBuilder.BuildWhere();
            return result.SqlString.ToString();
        }

        public string BuildOrderClause(DynamicQuery dynamicQuery)
        {
            var selectBuilder = ServiceProvider.GetRequiredService<ISelectBuilder>();
            selectBuilder.Mapping(BuildMappings());
            return selectBuilder.OrderBy(dynamicQuery.Order);
        }
    }
}
