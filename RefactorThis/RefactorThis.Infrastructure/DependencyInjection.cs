using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RefactorThis.Core.Common.Interfaces;
using RefactorThis.Infrastructure.Persistence;

namespace RefactorThis.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, bool useInMemory = false)
        {
            if (useInMemory)
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseInMemoryDatabase("CleanArchitectureDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(
                        configuration.GetConnectionString("Default")
                    );
                    options.EnableSensitiveDataLogging(true);
                });
            }

#pragma warning disable CS8603 // Possible null reference return.
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
#pragma warning restore CS8603 // Possible null reference return.

            return services;
        }
    }
}
