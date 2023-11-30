namespace EGAH.EventGenerator;

using EGAH.EventGenerator.Settings;
using EGAH.Services.Events;
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
            .AddEventService()
            ;

        return services;
    }
}
