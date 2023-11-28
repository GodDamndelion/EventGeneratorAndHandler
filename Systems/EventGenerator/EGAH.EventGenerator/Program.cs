using EGAH.EventGenerator.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger();

var services = builder.Services;

services.AddHttpContextAccessor();
services.AddAppCors();

services.AddAppHealthChecks();
services.AddAppVersioning();
services.AddAppSwagger();

services.AddAppControllers();

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();
app.UseAppSwagger();

app.UseAppControllers();

app.Run();
