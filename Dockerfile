# BUILD STAGE
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Kopiraj .csproj fajlove
COPY src/Tours.Api/*.csproj src/Tours.Api/
COPY src/Tours.Core/*.csproj src/Tours.Core/
COPY src/Tours.Infrastructure/*.csproj src/Tours.Infrastructure/

# Kopiraj solution fajl
COPY *.sln ./

# Restore dependencies
RUN dotnet restore

# Kopiraj ostatak koda
COPY . .

# Publish samo API (jer on vuče ostale)
WORKDIR /src/src/Tours.Api
RUN dotnet publish -c Release -o /app/publish

# RUNTIME STAGE
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Tours.Api.dll"]
