using eCommerce.Domian.Interface;
using eCommerce.InfraStructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.InfraStructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(options =>
            {
                var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();  
                options.UseNpgsql(configuration.GetConnectionString("CONNECTION_STRING"));
            });
            services.AddScoped<IUserRepository,UserRepo>();
            return services;

        }
    }
}
