using FeiniuBus.DynamicQ.Linq;
using FeiniuBus.Test.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FeiniuBus.Test
{
    public partial  class Testing
    {
        [Fact]
        public void TestDynamicQ()
        {
            var services = new ServiceCollection();
            services.AddLinqConverter();

            var provider = services.BuildServiceProvider();
            var converter = provider.GetRequiredService<LinqQueryConverter>();

            var builder = FeiniuBus.DynamicQ.Builder.DynamicQueryBuilder.Create(true);
            var child1 = builder.ParamGroupBuilder.CreateChildAndGroup();
            child1.ParamBuilder.Any("Extras", sub => { sub.ParamBuilder.Equal("Guest", "Andy"); });
            var child2 = builder.ParamGroupBuilder.CreateChildAndNotGroup()
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
            builder.OrderBy("test_address", System.ComponentModel.ListSortDirection.Ascending)
                .Take(10);
            var dynamicQuery = builder.Build();
            var result = converter.Convert(DynamicQ.Infrastructure.ClientTypes.EntityFramework, dynamicQuery,typeof(TestingDto), true);
            //Assert.NotNull(result);
        }
    }
}
