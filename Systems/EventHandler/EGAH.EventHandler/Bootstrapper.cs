namespace EGAH.EventHandler;

using EGAH.EventHandler.Settings;
using EGAH.Services.Incidents;
using EGAH.Services.Settings;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            .AddSwaggerSettings()
            .AddApiSpecialSettings()
            .AddIncidentService()
            ;

        return services;
    }
}
