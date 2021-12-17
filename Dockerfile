# Use SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# Copy everything and build the app
COPY src/Api/. .
RUN dotnet publish -c Release -o /app

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy build results and run the app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "PicoStation.Api.dll", "--urls", "http://+:5030"]