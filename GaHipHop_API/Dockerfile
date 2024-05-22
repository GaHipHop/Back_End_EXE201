#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["GaHipHop_API/GaHipHop_API.csproj", "GaHipHop_API/"]
COPY ["GaHipHop_Model/GaHipHop_Model.csproj", "GaHipHop_Model/"]
COPY ["GaHipHop_Repository/GaHipHop_Repository.csproj", "GaHipHop_Repository/"]
COPY ["GaHipHop_Service/GaHipHop_Service.csproj", "GaHipHop_Service/"]
COPY ["Tools/Tools.csproj", "Tools/"]
RUN dotnet restore "./GaHipHop_API/GaHipHop_API.csproj"
COPY . .
WORKDIR "/src/GaHipHop_API"
RUN dotnet build "./GaHipHop_API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./GaHipHop_API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GaHipHop_API.dll"]