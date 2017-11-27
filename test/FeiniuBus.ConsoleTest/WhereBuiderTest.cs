using System;
using FeiniuBus.SqlBuilder;
using FeiniuBus.SqlBuilder.Mysql;

namespace FeiniuBus.ConsoleTest
{
    public class WhereBuiderTest
    {
        public static void Test()
        {
            IWhereBuilder builder = new MysqlWhereBuilder(new DefaultCharacterConverter());
            builder.Mapping(new SqlFieldMappings
            {
                {"Id", "Id"},
                {"User.Id", "a.Id"},
                {"Test.Name", "a.Id"}
            });
            builder.Where(bu =>
            {
                bu.ParamBuilder.Equal("User.Id", "0000101");
                bu.ParamBuilder.StartsWith("User.Id", "0000101");
                bu.ParamBuilder.In("User.Id", "1,2,3,4");
                bu.ParamBuilder.Any("Test", s => s.ParamBuilder.Equal("Name", "123"));
            });

            Console.WriteLine(builder.BuildWhere().ToString());


            IDeleteBuilder deleteBuilder = new MysqlDeleteBuilder(new DefaultCharacterConverter());
            deleteBuilder.Delete("TestTable").Where(i => i.ParamBuilder.Equal("Id", 100).Equal("Id", 200));

            Console.WriteLine(deleteBuilder.Build());
        }
    }
}