FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY src/Tours.Api/*.csproj src/Tours.Api/
COPY src/Tours.Core/*.csproj src/Tours.Core/
COPY src/Tours.Infrastructure/*.csproj src/Tours.Infrastructure/
COPY *.sln ./
RUN dotnet restore
COPY . .
WORKDIR /src/src/Tours.Api
# Debug build da bi attach radio lepo sa simbolima
RUN dotnet publish -c Debug -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
# vsdbg nije obavezan; možeš obrisati sledeći blok ako želiš
# RUN apt-get update && apt-get install -y unzip curl \
#     && curl -sSL https://aka.ms/getvsdbgsh | bash /dev/stdin -v latest -l /vsdbg \
#     && rm -rf /var/lib/apt/lists/*
EXPOSE 8080
ENTRYPOINT ["dotnet", "Tours.Api.dll"]