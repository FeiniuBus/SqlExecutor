using FeiniuBus.DynamicQ.Converter;
using FeiniuBus.DynamicQ.Internal;
using FeiniuBus.DynamicQ.Linq.Converters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FeiniuBus.DynamicQ.Linq
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddLinqConverter(this IServiceCollection services)
        {
            services.TryAddSingleton<IPropertyAccessor, PropertyAccessor>();

            services.AddScoped<IQueryConverter<LinqConvertResult>, LinqQueryConverter>();
            //services.TryAddScoped<LinqQueryConverter>();

            services.AddScoped<LinqContext>();

            services.AddScoped<IOperationConverter, OperationAnyConverter>();
            services.AddScoped<IOperationConverter, OperationContainsConverter>();
            services.AddScoped<IOperationConverter, OperationEndsWithConverter>();
            services.AddScoped<IOperationConverter, OperationEqualConverter>();
            services.AddScoped<IOperationConverter, OperationGreaterConverter>();
            services.AddScoped<IOperationConverter, OperationGreaterThanOrEqualConverter>();
            services.AddScoped<IOperationConverter, OperationInConverter>();
            services.AddScoped<IOperationConverter, OperationLessThanConverter>();
            services.AddScoped<IOperationConverter, OperationLessThanOrEqualConverter>();
            services.AddScoped<IOperationConverter, OperationStartsWithConverter>();
            services.AddScoped<IParameterGroupConverter, ParameterGroupConverter>();
            services.AddScoped<IRelationConverter, RelationAndConverter>();
            services.AddScoped<IRelationConverter, RelationAndNotConverter>();
            services.AddScoped<IRelationConverter, RelationOrConverter>();
            services.AddScoped<IRelationConverter, RelationOrNotConverter>();

            return services;
        }
    }
}