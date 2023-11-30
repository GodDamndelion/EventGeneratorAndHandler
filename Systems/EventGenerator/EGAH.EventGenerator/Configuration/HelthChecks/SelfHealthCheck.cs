namespace EGAH.EventGenerator.Configuration;

using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;

public class SelfHealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var assembly = Assembly.Load("EGAH.EventGenerator");
        var versionNumber = assembly.GetName().Version;

        return Task.FromResult(HealthCheckResult.Healthy(description: $"Build {versionNumber}"));
    }
    //Признаки жизни сервисов и приложений могут быть разные
    //Присылает ли ответ
    //Время ответа (Если долгое - поднять ещё один, например)
    //Есть ли связь с БД (SELECT 1 + 1)(Если пришло 2, значит оба живы)
}