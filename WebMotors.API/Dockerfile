# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebMotors.API/WebMotors.API.csproj", "WebMotors.API/"]
COPY ["WebMotors.Domain.Shared/WebMotors.Domain.Shared.csproj", "WebMotors.Domain.Shared/"]
COPY ["WebMotors.Infra.CrossCutting.IoC/WebMotors.Infra.CrossCutting.IoC.csproj", "WebMotors.Infra.CrossCutting.IoC/"]
COPY ["WebMotors.Application/WebMotors.Application.csproj", "WebMotors.Application/"]
COPY ["WebMotors.Domain/WebMotors.Domain.csproj", "WebMotors.Domain/"]
COPY ["WebMotors.Data/WebMotors.Data.csproj", "WebMotors.Data/"]
RUN dotnet restore "./WebMotors.API/WebMotors.API.csproj"
COPY . .
WORKDIR "/src/WebMotors.API"
RUN dotnet build "./WebMotors.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebMotors.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebMotors.API.dll"]