using Discount.Core.Contracts;
using Discount.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Discount.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}
