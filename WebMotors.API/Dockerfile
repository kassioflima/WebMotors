#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["WebMotors.API/WebMotors.API.csproj", "WebMotors.API/"]
RUN dotnet restore "WebMotors.API/WebMotors.API.csproj"
COPY . .
WORKDIR "/src/WebMotors.API"
RUN dotnet build "WebMotors.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebMotors.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebMotors.API.dll"]