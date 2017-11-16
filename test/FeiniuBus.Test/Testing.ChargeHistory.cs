using FeiniuBus.LinqBuilder;
using System.Linq;
using Xunit;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
		[Fact]
		public void TestChargeHistory()
        {
            var query = new Model.ChargeHostory[]
            {
                new Model.ChargeHostory(),
                new Model.ChargeHostory(),
            }.AsQueryable();

            var builder = new DynamicQueryBuilder(true);
            builder.Take(20).Skip(10).ParamGroupBuilder.ParamBuilder.Equal("charge_id", 1);

            var converter = new QueryConverter(typeof(Model.ChargeHostory), builder.Build(), true);
            var model = converter.Converter();
        }
    }
}
