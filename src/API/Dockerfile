#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application.CQRS.Base/Application.CQRS.Base.csproj", "Application.CQRS.Base/"]
COPY ["Application.CQRS.Command/Application.CQRS.Command.csproj", "Application.CQRS.Command/"]
COPY ["Application.CQRS.Query.SQL/Application.CQRS.Query.SQL.csproj", "Application.CQRS.Query.SQL/"]
COPY ["Infrastructure.Base/Infrastructure.Base.csproj", "Infrastructure.Base/"]
COPY ["Infrastructure.SQLServer/Infrastructure.SQLServer.csproj", "Infrastructure.SQLServer/"]
COPY ["Infrastructure.MongoDB/Infrastructure.MongoDB.csproj", "Infrastructure.MongoDB/"]
COPY ["Infrastructure.Redis/Infrastructure.Redis.csproj", "Infrastructure.Redis/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]


ARG AMBIENTE
RUN echo ${AMBIENTE}

COPY ["API/Config/appsettings.$AMBIENTE.json" , "API/appsettings.json"]


RUN dotnet restore "./API/API.csproj"
COPY . .
WORKDIR "/src/API"
RUN dotnet build "./API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "API.dll"]