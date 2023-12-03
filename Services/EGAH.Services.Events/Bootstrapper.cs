namespace EGAH.Services.Events;

using EGAH.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    /// <summary>
    /// Register EventService
    /// </summary>
    public static IServiceCollection AddEventService(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = Settings.Load<EventServiceSettings>("EventService", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IEventService, EventService>();

        return services;
    }
}
