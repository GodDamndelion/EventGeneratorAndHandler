namespace EGAH.EventGenerator.Configuration;

using Serilog;

/// <summary>
/// Logger Configuration
/// </summary>
public static class LoggerConfiguration
{
    /// <summary>
    /// Add logger
    /// </summary>
    public static void AddAppLogger(this WebApplicationBuilder builder)
    {
        var logger = new Serilog.LoggerConfiguration()
            .Enrich.WithCorrelationIdHeader() //Дописывается CorrelationId, чтобы понимать, какой ответ к какому запросу
            .Enrich.FromLogContext()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog(logger, true);
    }
}