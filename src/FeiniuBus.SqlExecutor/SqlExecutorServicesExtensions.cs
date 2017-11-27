using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlExecutor
{
    public static class SqlExecutorServicesExtensions
    {
        public static IServiceCollection AddSqlExecutor<TSqlExecutor>(this IServiceCollection services,
            SqlExecutorOptions options)
            where TSqlExecutor : class, ISqlExecutor
        {
            services.AddSqlExecutor<ISqlExecutor, TSqlExecutor>(options);

            return services;
        }


        public static IServiceCollection AddSqlExecutor<TISqlExecutor, TSqlExecutor>(this IServiceCollection services,
            SqlExecutorOptions options)
            where TSqlExecutor : class, TISqlExecutor
            where TISqlExecutor : class, ISqlExecutor
        {
            services.AddSingleton<ISqlExecutorFactory, SqlExecutorFactory>();
            services.AddTransient<TISqlExecutor, TSqlExecutor>();
            services.AddSingleton(options);
            return services;
        }
    }
}