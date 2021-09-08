using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Data.Context;
using WebMotors.Domain.Shared.Querys;

namespace WebMotors.API.Configurations
{
    /// <summary>
    /// Configuration Database Context
    /// </summary>
    public static class DatabaseConfig
    {
        /// <summary>
        /// Add Context in startup.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddContextConfiguration(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<AppSettingsQueryResult>(Configuration.GetSection("ConnectionStrings"));

            var connection = Configuration["ConnectionStrings:WebMotorsContext"];

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            return services;
        }
    }
}
