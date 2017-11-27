using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FeiniuBus.LinqBuilder;
using FeiniuBus.Test.Model;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
        [Fact]
        public void Test()
        {
            var dynamicQuery = BuildDynamicQuery();
            var whereClause = BuilderWhereClause(dynamicQuery);
            var orderClause = BuildOrderClause(dynamicQuery);

            // Get data ready
            var context = ServiceProvider.GetRequiredService<Context>();
            context.TestingDto.Add(new TestingDto
            {
                Id = Guid.NewGuid().ToString("N"),
                TestAddress = "chengdu lnk",
                Disabled = false,
                Amout = 11,
                Price = 110,
                Drink = "coffee",
                Count = 1,
                Total = 100,
                Url = "Http://www.feiniubus.com",
                Extras = new HashSet<TestingDto.Extra>
                {
                    new TestingDto.Extra
                    {
                        Guest = "Andy"
                    }
                }
            });
            context.SaveChanges();

            //testing
            var entityType = typeof(TestingDto);
            var converter = new QueryConverter(entityType, dynamicQuery, true);
            var model = converter.Converter();
            var entities = AutoQueryAsync(context.TestingDto.AsQueryable(), model, true);

            //sql testing
            var sb = new StringBuilder();
            sb.AppendLine("SELECT * FROM TestingDto WHERE");
            sb.AppendLine(whereClause.Item1);
            sb.AppendLine("ORDER BY");
            sb.AppendLine(orderClause);
            sb.AppendLine(";");
            // ReSharper disable once UnusedVariable
            var sql = sb.ToString();


            Assert.True(entities?.Rows?.Count() == 0);
        }
    }
}