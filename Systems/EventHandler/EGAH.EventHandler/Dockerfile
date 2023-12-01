#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/EventHandler/EGAH.EventHandler/EGAH.EventHandler.csproj", "Systems/EventHandler/EGAH.EventHandler/"]
COPY ["Data/EGAH.Context/EGAH.Context.csproj", "Data/EGAH.Context/"]
COPY ["Shared/EGAH.Settings/EGAH.Settings.csproj", "Shared/EGAH.Settings/"]
COPY ["Data/EGAH.Context.Entities/EGAH.Context.Entities.csproj", "Data/EGAH.Context.Entities/"]
COPY ["Data/Migrations/EGAH.Context.MigrationsPostgreSQL/EGAH.Context.MigrationsPostgreSQL.csproj", "Data/Migrations/EGAH.Context.MigrationsPostgreSQL/"]
COPY ["Services/EGAH.Services.Events/EGAH.Services.Events.csproj", "Services/EGAH.Services.Events/"]
COPY ["Shared/EGAH.Common/EGAH.Common.csproj", "Shared/EGAH.Common/"]
COPY ["Services/EGAH.Services.Incidents/EGAH.Services.Incidents.csproj", "Services/EGAH.Services.Incidents/"]
COPY ["Services/EGAH.Services.Settings/EGAH.Services.Settings.csproj", "Services/EGAH.Services.Settings/"]
COPY ["Systems/EventHandler/EGAH.EventHandler.Settings/EGAH.EventHandler.Settings.csproj", "Systems/EventHandler/EGAH.EventHandler.Settings/"]
RUN dotnet restore "Systems/EventHandler/EGAH.EventHandler/EGAH.EventHandler.csproj"
COPY . .
WORKDIR "/src/Systems/EventHandler/EGAH.EventHandler"
RUN dotnet build "EGAH.EventHandler.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EGAH.EventHandler.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EGAH.EventHandler.dll"]