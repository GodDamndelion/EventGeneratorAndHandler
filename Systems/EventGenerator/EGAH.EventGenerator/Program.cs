using EGAH.Context;
using EGAH.EventGenerator;
using EGAH.EventGenerator.Configuration;
using EGAH.EventGenerator.Work;
using EGAH.Services.Events;
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

services.AddHttpClient();

services.AddEventService(builder.Configuration);

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();
app.UseAppSwagger();

app.UseAppControllers();

DbInitializer.Execute(app.Services);
DbSeeder.Execute(app.Services, true);

Task work = Task.Run(() => Work.Do(app.Services));

app.Run();
