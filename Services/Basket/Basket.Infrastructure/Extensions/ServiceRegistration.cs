using Basket.Core.Contracts;
using Basket.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();

            return services;
        }
    }
}
