using System.Linq;
using FeiniuBus.LinqBuilder;
using FeiniuBus.Test.Model;
using Xunit;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
        [Fact]
        public void TestChargeHistory()
        {
            var query = new[]
            {
                new ChargeHostory(),
                new ChargeHostory()
            }.AsQueryable();

            var builder = new DynamicQueryBuilder(true);
            builder.Take(20).Skip(10).ParamGroupBuilder.ParamBuilder.Equal("charge_id", 1);

            var converter = new QueryConverter(typeof(ChargeHostory), builder.Build(), true);
            var model = converter.Converter();
        }
    }
}