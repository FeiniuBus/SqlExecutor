using System;
using System.Collections.Generic;
using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Linq;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace FeiniuBus.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var services = new ServiceCollection();
            services.AddLinqConverter();

            var provider = services.BuildServiceProvider();
            var converter = provider.GetRequiredService<IQueryConverter<LinqConvertResult>>();

            var builder = FeiniuBus.DynamicQ.Builder.DynamicQueryBuilder.Create(true);
            var child1 = builder.ParamGroupBuilder.CreateChildAndGroup();
            child1.ParamBuilder.Not(not =>
                not.ParamBuilder.Any("Extras", sub => { sub.ParamBuilder.Equal("Guest", "Andy"); }));
            //child1.ParamBuilder;
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
            builder.OrderBy("test_address", System.ComponentModel.ListSortDirection.Ascending).Take(10);
            var dynamicQuery = builder.Build();
            var result = converter.Convert(DynamicQ.Infrastructure.ClientTypes.EntityFramework, dynamicQuery,
                typeof(TestingDto), true);
            Console.ReadLine();
        }
    }

    public class TestingDto
    {
        public TestingDto()
        {
            Extras = new HashSet<Extra>();
        }

        public string Id { get; set; }

        [JsonProperty("test_address")]
        public string TestAddress { get; set; }

        public bool Disabled { get; set; }
        public int Amout { get; set; }
        public double Price { get; set; }
        public string Drink { get; set; }
        public int Count { get; set; }
        public int Total { get; set; }
        public string Url { get; set; }

        public HashSet<Extra> Extras { get; set; }

        public class Extra
        {
            public string Guest { get; set; }
        }
    }
}