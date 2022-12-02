using Microsoft.Extensions.DependencyInjection;
using TrendyolStoreInfo.Services;

namespace TrendyolStoreInfo
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTrendyolStoreService(this IServiceCollection services)
        { 
            services.AddScoped<ITrendyolStoreService, TrendyolStoreService>();

            return services;
        }
    }
}
