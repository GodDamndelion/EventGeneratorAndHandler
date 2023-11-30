namespace EGAH.Services.Incidents;

using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddIncidentService(this IServiceCollection services)
    {
        services.AddSingleton<IIncidentService, IncidentService>();

        return services;
    }
}
