using eCommerce.Core.Interfaces;
using eCommerce.Core.Services;
using eCommerce.Core.Validatiors;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddValidatorsFromAssemblyContaining<LoginRequestValidatiors>();
            services.AddValidatorsFromAssemblyContaining<RegisterRequestValidators>();
            return services;
        }
    }
}
