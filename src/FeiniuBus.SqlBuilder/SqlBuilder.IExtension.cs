using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlBuilder
{
    public interface ISqlBuilderExtension
    {
         void AddServices(IServiceCollection services);
    }
}
