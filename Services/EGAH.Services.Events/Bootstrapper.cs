namespace EGAH.Services.Events;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddEventService(this IServiceCollection services)
    {
        services.AddSingleton<IEventService, EventService>();

        return services;
    }
}
