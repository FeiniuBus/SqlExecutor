using FeiniuBus.SqlBuilder;
using FeiniuBus.SqlBuilder.Mysql;
using Xunit;

namespace FeiniuBus.Test
{

    public class WhereBuiderTest
    {
        [Fact]
        public void Test()
        {
            IWhereBuilder builder = new MysqlWhereBuilder(new DefaultCharacterConverter());
            builder.Where(bu =>
            {

                
            });


        }
    }
}
