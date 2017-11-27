using System;
using System.Text;
using FeiniuBus;
using FeiniuBus.SqlBuilder;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Feiniubus.DynamicQuery.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSQLBuilder(opts => { opts.UseMySQL(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.Run(async context =>
            {
                var builder = DynamicQueryBuilder.Create(true);
                var child1 = builder.ParamGroupBuilder.CreateChildAndGroup();
                child1.ParamBuilder.Any("Extra", sub => { sub.ParamBuilder.Equal("Guest", "Andy"); });
                var child2 = builder.ParamGroupBuilder.CreateChildOrGroup()
                    .ParamBuilder
                    .Contains("Address", "chengdu")
                    .EndsWith("Address", "lnk")
                    .Equal("Disabled", false)
                    .GreaterThan("Amout", 10)
                    .GreaterThanOrEqual("Price", 100)
                    .In("Drink", "mileshake,coffee")
                    .LessThan("Count", 10)
                    .LessThanOrEqual("Total", 100)
                    .StartsWith("Url", "Http://");
                builder.OrderBy("Amout", ListSortDirection.Ascending)
                    .Select("Guest").Take(10).Skip(10);
                var dynamicQuery = builder.Build();


                SqlBuilderSample(dynamicQuery, context.RequestServices.GetRequiredService<ISelectBuilder>());
            });
        }


        public void SqlBuilderSample(FeiniuBus.DynamicQuery dynamicQuery, ISelectBuilder selectBuilder)
        {
            var sb = new StringBuilder();
            var mappings = new SqlFieldMappings();
            mappings.Map("Guest", "t1.Guest")
                .Map("Address", "t1.Address")
                .Map("Disabled", "t1.Disabled")
                .Map("Amout", "t1.Amout")
                .Map("Price", "t1.Price")
                .Map("Drink", "t1.Drink")
                .Map("Count", "t1.Count")
                .Map("Total", "t1.Total")
                .Map("Url", "t1.Url");
            selectBuilder.Mapping(mappings);
            selectBuilder.Where(dynamicQuery.ParamGroup);

            var whereClause = selectBuilder.BuildWhere();
            var orderbyClause = selectBuilder.OrderBy(dynamicQuery.Order);

            Console.WriteLine(whereClause);
            Console.WriteLine(orderbyClause);
        }
    }
}