using FeiniuBus;
using FeiniuBus.SqlBuilder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddSQLBuilder(this IServiceCollection services, Action<SqlBuilderOptions> configure)
        {
            var options = new SqlBuilderOptions();
            configure(options);

            services.AddSingleton(options);
            services.AddScoped<ICharacterConverter, DefaultCharacterConverter>();
            services.AddScoped<ISqlMapper, SqlMapper>();

            foreach (var extension in options.Extensions)
            {
                extension.AddServices(services);
            }
        }
    }
}
