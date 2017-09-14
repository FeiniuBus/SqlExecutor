using System;
using System.Collections.Generic;
using System.Text;
using FeiniuBus.SqlBuilder.Mysql;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlBuilder.Extensions
{
    public static class SqlBuilderExtensions
    {
        public static IServiceCollection AddMysqlBuilder(this IServiceCollection services)
        {
            services.AddScoped<ISelectBuilder, MysqlSelectBuilder>();


            return services;
        }
    }
}
