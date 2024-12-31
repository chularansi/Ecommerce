using Catalog.Application.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductMapper).Assembly);

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly));

            return services;
        }
    }
}
