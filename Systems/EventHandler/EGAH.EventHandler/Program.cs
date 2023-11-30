using EGAH.Context;
using EGAH.EventHandler;
using EGAH.EventHandler.Configuration;
using EGAH.Services.Settings;
using EGAH.Settings;

var builder = WebApplication.CreateBuilder(args);

var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger(swaggerSettings);
services.AddAppAutoMappers();

services.AddAppControllers();

services.RegisterAppServices();

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();
app.UseAppSwagger();

app.UseAppControllers();

app.Run();
