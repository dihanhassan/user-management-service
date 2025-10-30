# -------------------------------
# 1. Build Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution file
COPY UserVault.sln .

# Copy all projects
COPY UserVault.Application/UserVault.Application.csproj Core/UserVault.Application/
COPY UserVault.Domain/UserVault.Domain.csproj Core/UserVault.Domain/
COPY UserVault.Cache/UserVault.Cache.csproj Infrastructure/UserVault.Cache/
COPY UserVault.Data/UserVault.Data.csproj Infrastructure/UserVault.Data/
COPY UserVault.Infra/UserVault.Infrastructure.csproj UserVault.Infra/
COPY UserVault/UserVault.csproj UserVault/
# Restore dependencies
RUN dotnet restore UserVault/UserVault.csproj

# Copy all other source code
COPY . .

# Build project
RUN dotnet publish UserVault/UserVault.csproj -c Release -o /app/publish


# -------------------------------
# 2. Runtime Stage
# -------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

COPY --from=build /app/publish .

# ✅ Force ASP.NET Core to run on port 7202 inside docker
ENV ASPNETCORE_URLS=http://+:7202

EXPOSE 7202

ENTRYPOINT ["dotnet", "UserVault.dll"]
