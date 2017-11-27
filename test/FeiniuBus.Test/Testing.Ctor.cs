using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.Test
{
    public partial class Testing
    {
        public Testing()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSQLBuilder(opts => opts.UseMySQL());
            services.AddDbContext<Context>(opts => opts.UseInMemoryDatabase("TestDB"));
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}