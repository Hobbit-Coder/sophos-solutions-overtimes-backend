using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Repositories;
using SophosSolutions.Overtimes.Application.Common.Interfaces.Services;
using SophosSolutions.Overtimes.Infrastructure.Identity;
using SophosSolutions.Overtimes.Infrastructure.Persistence.Contexts;
using SophosSolutions.Overtimes.Infrastructure.Services;
using SophosSolutions.Overtimes.Models.Entities;

namespace SophosSolutions.Overtimes.Infrastructure;

public static class Configuration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OvertimesDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("OvertimesDb"));
        });

        services.AddIdentityCore<User>()
            .AddRoles<Role>()
            .AddEntityFrameworkStores<OvertimesDbContext>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IEmailService, SmtpService>();

        services.AddScoped<OvertimesDbContextSeed>();

        return services;
    }
}
