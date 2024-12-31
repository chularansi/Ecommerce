using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Contracts;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(
                configuration.GetConnectionString("OrderingConnectionString"),
                sqlServerOptions => sqlServerOptions.EnableRetryOnFailure()));

            services.AddScoped(typeof(IGenericRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
