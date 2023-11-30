namespace EGAH.Context;

using Microsoft.EntityFrameworkCore;

public static class DbContextOptionsFactory
{
    private const string migrationProjctPrefix = "EGAH.Context.Migrations"; //Название проекта, в который будут складываться миграции

    public static DbContextOptions<MainDbContext> Create(string connStr)
    {
        var bldr = new DbContextOptionsBuilder<MainDbContext>();

        Configure(connStr).Invoke(bldr);

        return bldr.Options;
    }

    public static Action<DbContextOptionsBuilder> Configure(string connStr)
    {
        return (bldr) =>
        {
            bldr.UseNpgsql(connStr,
                opts => opts
                        .CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds)
                        .MigrationsHistoryTable("_EFMigrationsHistory", "public")
                        .MigrationsAssembly($"{migrationProjctPrefix}PostgreSQL") //Путь для складывания миграций
                );

            bldr.EnableSensitiveDataLogging();
            bldr.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        };
    }
}
