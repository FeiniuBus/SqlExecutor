using System;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.SqlExecutor
{
    public interface ISqlExecutorFactory
    {
        ISqlExecutor Create();
    }

    public class SqlExecutorFactory : ISqlExecutorFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public SqlExecutorFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ISqlExecutor Create()
        {
            var services = _serviceProvider.GetService<ISqlExecutor>();
            if (services == null)
                throw new Exception("未注册ISqlExecutor实例");
            return services;
        }
    }
}