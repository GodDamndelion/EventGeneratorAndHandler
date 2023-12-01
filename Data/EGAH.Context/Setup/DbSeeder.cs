namespace EGAH.Context;

using EGAH.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbSeeder
{
    private static IServiceScope ServiceScope(IServiceProvider serviceProvider) => serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
    private static MainDbContext DbContext(IServiceProvider serviceProvider) => ServiceScope(serviceProvider).ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();

    public static void Execute(IServiceProvider serviceProvider, bool addDemoData)
    {
        using var scope = ServiceScope(serviceProvider);
        ArgumentNullException.ThrowIfNull(scope);

        if (addDemoData)
        {
            ConfigureDemoData(scope);
        }
    }

    private static void ConfigureDemoData(IServiceScope scope)
    {
        AddDemoData(scope);
    }

    private static void AddDemoData(IServiceScope scope)
    {
        // Асинхронность тут не работает.
        using var context = DbContext(scope.ServiceProvider);

        if (context.Incidents.Any() || context.Events.Any())
            return;

        // Можно добавить сюда начальные данные в БД

        context.SaveChanges();
    }
}
