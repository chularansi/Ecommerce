using Basket.Application.Mappers;
using Basket.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(BasketMapper).Assembly);

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));
            services.AddScoped<DiscountGrpcService>();

            return services;
        }
    }
}
