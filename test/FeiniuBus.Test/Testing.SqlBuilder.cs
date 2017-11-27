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
                .Map("TestAddress", "t1.Address")
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

        public SqlFieldMappings BuildMappings2()
        {
            var mappings = new SqlFieldMappings();
            mappings.Map("zxc", "t1.Address")
                .Map("fgh", "t1.Disabled")
                .Map("asd", "t1.Amout")
                .Map("rty", "t1.Price")
                .Map("wer", "t1.Drink")
                .Map("qwe", "t1.Count")
                .Map("Abc", "t1.Total");
            // Native api also be supported
            mappings.Add("Url", "t1.Url");
            return mappings;
        }

        public (string, string) BuilderWhereClause(DynamicQuery dynamicQuery)
        {
            var selectBuilder = ServiceProvider.GetRequiredService<ISelectBuilder>();
            selectBuilder.Mapping(BuildMappings());
            selectBuilder.Where(dynamicQuery.ParamGroup);
            var result = selectBuilder.BuildWhere();

            var selectBuilder2 = selectBuilder.CreateScope();
            selectBuilder2.Mapping(BuildMappings());
            selectBuilder2.Where(dynamicQuery.ParamGroup);
            var result2 = selectBuilder2.BuildWhere();

            return (result.SqlString.ToString(), result2.SqlString.ToString());
        }

        public string BuildOrderClause(DynamicQuery dynamicQuery)
        {
            var selectBuilder = ServiceProvider.GetRequiredService<ISelectBuilder>();
            selectBuilder.Mapping(BuildMappings());
            return selectBuilder.OrderBy(dynamicQuery.Order);
        }
    }
}