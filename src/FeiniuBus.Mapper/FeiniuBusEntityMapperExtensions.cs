using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FeiniuBus.Mapper
{
    public static class FeiniuBusEntityMapperExtensions
    {
        public static IServiceCollection AddEntityMapper(this IServiceCollection services)
        {
            services.AddSingleton<IFeiniuBusMapperInitialize, FeiniuBusMapperInitialize>();
            services.AddScoped<IEntityMapper, FeiniuBusEntityMapper>();
            return services;
        }

        public static IServiceCollection AddMapperConfig<TMapperConfig>(this IServiceCollection services)
            where TMapperConfig : class, IMapperConfig
        {
            services.AddScoped<IMapperConfig, TMapperConfig>();
            return services;
        }


        public static IApplicationBuilder UseEntityMapper(this IApplicationBuilder app)
        {
            var ser = app.ApplicationServices.GetService<IFeiniuBusMapperInitialize>();
            if (ser == null)
                throw new Exception("请先调用AddEntityMapper方法");

            var cofs = app.ApplicationServices.GetServices<IMapperConfig>();
            ser.AddMapperConfigs(cofs);
            ser.Initialize();
            return app;
        }
    }
}
