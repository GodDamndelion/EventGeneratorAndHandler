using EGAH.EventGenerator.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger();

builder.Services.AddControllers();

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();
app.UseAppSwagger();

app.UseAuthorization();

app.MapControllers();

app.Run();
