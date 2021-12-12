# Use SDK image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy everything and build the app
COPY src/Api/. ./Api/
RUN dotnet publish Api -c Release -o out

# Use runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

# Copy build results and run the app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "PicoStation.Api.dll", "--urls", "http://+:5030"]