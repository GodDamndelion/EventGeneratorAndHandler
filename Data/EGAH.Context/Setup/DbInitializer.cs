namespace EGAH.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

public static class DbInitializer
{
    public static void Execute(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.GetService<IServiceScopeFactory>()?.CreateScope();
        ArgumentNullException.ThrowIfNull(scope);

        var dbContextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>();
        using var context = dbContextFactory.CreateDbContext();
        context.Database.Migrate(); //Автомигрирование БД (Возможны ошибки)(Не забывать, что миграции применяются последовательно)
    }
}
