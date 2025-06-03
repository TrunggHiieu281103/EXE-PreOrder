# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY EXE_PreOrders.sln ./

# Copy all projects (correct folder names)
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure.Identity/Infrastructure.Identity.csproj Infrastructure.Identity/
COPY Infrastructure.Persistence/Infrastructure.Persistence.csproj Infrastructure.Persistence/
COPY Infrastructure.Shared/Infrastructure.Shared.csproj Infrastructure.Shared/
COPY WebApi/WebApi.csproj WebApi/

# Restore dependencies
RUN dotnet restore EXE_PreOrders.sln
COPY . .
WORKDIR /src/WebApi
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENTRYPOINT ["dotnet", "WebApi.dll"]
