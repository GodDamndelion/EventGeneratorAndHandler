#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/EventGenerator/EGAH.EventGenerator/EGAH.EventGenerator.csproj", "Systems/EventGenerator/EGAH.EventGenerator/"]
COPY ["Services/EGAH.Services.Settings/EGAH.Services.Settings.csproj", "Services/EGAH.Services.Settings/"]
COPY ["Shared/EGAH.Settings/EGAH.Settings.csproj", "Shared/EGAH.Settings/"]
COPY ["Shared/EGAH.Common/EGAH.Common.csproj", "Shared/EGAH.Common/"]
COPY ["Systems/EventGenerator/EGAH.EventGenerator.Settings/EGAH.EventGenerator.Settings.csproj", "Systems/EventGenerator/EGAH.EventGenerator.Settings/"]
RUN dotnet restore "Systems/EventGenerator/EGAH.EventGenerator/EGAH.EventGenerator.csproj"
COPY . .
WORKDIR "/src/Systems/EventGenerator/EGAH.EventGenerator"
RUN dotnet build "EGAH.EventGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EGAH.EventGenerator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EGAH.EventGenerator.dll"]