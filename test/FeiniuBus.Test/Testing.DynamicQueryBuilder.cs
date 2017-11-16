namespace FeiniuBus.Test
{
    public partial class Testing
    {
        private DynamicQuery BuildDynamicQuery()
        {
            DynamicQueryBuilder builder = DynamicQueryBuilder.Create(true);
            var child1 = builder.ParamGroupBuilder.CreateChildAndGroup();
            child1.ParamBuilder.Any("Extras", sub =>
            {
                sub.ParamBuilder.Equal("Guest", "Andy");
            });
            var child2 = builder.ParamGroupBuilder.CreateChildAndGroup()
                        .ParamBuilder
                            .Contains("test_address", "chengdu")
                            .EndsWith("test_address", "lnk")
                            .Equal("Disabled", false)
                            .GreaterThan("Amout", 10)
                            .GreaterThanOrEqual("Price", 100)
                            .In("Drink", "mileshake,coffee")
                            .LessThan("Count", 10)
                            .LessThanOrEqual("Total", 100)
                            .StartsWith("Url", "Http://");
            builder.OrderBy("test_address", ListSortDirection.Ascending)
                .Take(10);
            var dynamicQuery = builder.Build();
            return dynamicQuery;
        }
    }
}
