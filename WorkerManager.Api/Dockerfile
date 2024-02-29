#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WorkerManager.Api/WorkerManager.Api.csproj", "WorkerManager.Api/"]
COPY ["WorkerManager.Infrastructure/WorkerManager.Infrastructure.csproj", "WorkerManager.Infrastructure/"]
COPY ["WorkerManager.Application/WorkerManager.Application.csproj", "WorkerManager.Application/"]
COPY ["WorkerManager.Domain/WorkerManager.Domain.csproj", "WorkerManager.Domain/"]
COPY ["WorkerManager.Shared.Abstractions/WorkerManager.Shared.Abstractions.csproj", "WorkerManager.Shared.Abstractions/"]
COPY ["WorkerManager.Shared/WorkerManager.Shared.csproj", "WorkerManager.Shared/"]
RUN dotnet restore "./WorkerManager.Api/./WorkerManager.Api.csproj"
COPY . .
WORKDIR "/src/WorkerManager.Api"
RUN dotnet build "./WorkerManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WorkerManager.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WorkerManager.Api.dll"]