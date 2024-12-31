using Discount.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Discount.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DiscountProfile).Assembly);

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            return services;
        }
    }
}
