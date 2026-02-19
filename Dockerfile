# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["zTools.API/zTools.API.csproj", "zTools.API/"]
COPY ["zTools.Application/zTools.Application.csproj", "zTools.Application/"]
COPY ["zTools.Domain/zTools.Domain.csproj", "zTools.Domain/"]
COPY ["zTools/zTools.csproj", "zTools/"]

RUN dotnet restore "zTools.API/zTools.API.csproj"

# Copy only necessary source code (exclude tests, docs, and sensitive files)
COPY ["zTools.API/", "zTools.API/"]
COPY ["zTools.Application/", "zTools.Application/"]
COPY ["zTools.Domain/", "zTools.Domain/"]
COPY ["zTools/", "zTools/"]

# Build the application (disable package generation for Docker build)
WORKDIR "/src/zTools.API"
RUN dotnet build "zTools.API.csproj" -c Release -o /app/build /p:GeneratePackageOnBuild=false

# Publish stage
FROM build AS publish
RUN dotnet publish "zTools.API.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:GeneratePackageOnBuild=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT=Docker
EXPOSE 8080

# Create a non-root user for security
RUN adduser --disabled-password --gecos '' --uid 1000 appuser && chown -R appuser /app
USER appuser

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "zTools.API.dll"]
