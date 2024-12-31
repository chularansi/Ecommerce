using Catalog.Core.Contracts;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Repositiories;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBrandRepository, ProductRepository>();
            services.AddScoped<ITypeRepository, ProductRepository>();

            return services;
        }
    }
}
