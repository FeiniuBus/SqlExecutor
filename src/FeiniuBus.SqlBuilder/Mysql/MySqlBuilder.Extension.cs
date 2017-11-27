using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlBuilder.Mysql
{
    public class MySqlBuilderExtension : ISqlBuilderExtension
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddScoped<ISqlBuilderFactory, MySqlBuilderFactory>();
            services.AddScoped<ISelectBuilder, MysqlSelectBuilder>();
            services.AddScoped<IWhereBuilder, MysqlWhereBuilder>();
            services.AddScoped<IOrderByBuider, MysqlSelectBuilder>();
        }
    }
}