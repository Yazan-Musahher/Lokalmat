# official Microsoft .NET SDK image for the build environment.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1
WORKDIR /src

# Copy solution file and all project files for dependency restoration.
COPY *.sln .
COPY API/LokalProdusert.API/*.csproj ./API/LokalProdusert.API/
COPY Modules/Sales/Sales.Application/*.csproj ./Modules/Sales/Sales.Application/
COPY Modules/Sales/Sales.Contracts/*.csproj ./Modules/Sales/Sales.Contracts/
COPY Modules/Sales/Sales.Domain/*.csproj ./Modules/Sales/Sales.Domain/
COPY Modules/Sales/Sales.Infrastructure/*.csproj ./Modules/Sales/Sales.Infrastructure/
COPY Modules/Sales/Sales.IntegrationEvents/*.csproj ./Modules/Sales/Sales.IntegrationEvents/
COPY Modules/Sales/Sales.API/*.csproj ./Modules/Sales/Sales.API/

COPY Modules/Payments/Payments.Application/*.csproj ./Modules/Payments/Payments.Application/
COPY Modules/Payments/Payments.Contracts/*.csproj ./Modules/Payments/Payments.Contracts/
COPY Modules/Payments/Payments.Domain/*.csproj ./Modules/Payments/Payments.Domain/
COPY Modules/Payments/Payments.Infrastructure/*.csproj ./Modules/Payments/Payments.Infrastructure/
COPY Modules/Payments/Payments.IntegrationEvents/*.csproj ./Modules/Payments/Payments.IntegrationEvents/
COPY Modules/Payments/Payments.API/*.csproj ./Modules/Payments/Payments.API/

COPY Modules/TransportHub/TransportHub.Application/*.csproj ./Modules/TransportHub/TransportHub.Application/
COPY Modules/TransportHub/TransportHub.Contracts/*.csproj ./Modules/TransportHub/TransportHub.Contracts/
COPY Modules/TransportHub/TransportHub.Domain/*.csproj ./Modules/TransportHub/TransportHub.Domain/
COPY Modules/TransportHub/TransportHub.Infrastructure/*.csproj ./Modules/TransportHub/TransportHub.Infrastructure/
COPY Modules/TransportHub/TransportHub.IntegrationEvents/*.csproj ./Modules/TransportHub/TransportHub.IntegrationEvents/
COPY Modules/TransportHub/TransportHub.API/*.csproj ./Modules/TransportHub/TransportHub.API/

COPY Modules/Users/Users.Application/*.csproj ./Modules/Users/Users.Application/
COPY Modules/Users/Users.Contracts/*.csproj ./Modules/Users/Users.Contracts/
COPY Modules/Users/Users.Domain/*.csproj ./Modules/Users/Users.Domain/
COPY Modules/Users/Users.Infrastructure/*.csproj ./Modules/Users/Users.Infrastructure/
COPY Modules/Users/Users.IntegrationEvents/*.csproj ./Modules/Users/Users.IntegrationEvents/
COPY Modules/Users/Users.API/*.csproj ./Modules/Users/Users.API/

COPY Modules/Administration/Administration.Application/*.csproj ./Modules/Administration/Administration.Application/
COPY Modules/Administration/Administration.Contracts/*.csproj ./Modules/Administration/Administration.Contracts/
COPY Modules/Administration/Administration.Domain/*.csproj ./Modules/Administration/Administration.Domain/
COPY Modules/Administration/Administration.Infrastructure/*.csproj ./Modules/Administration/Administration.Infrastructure/
COPY Modules/Administration/Administration.IntegrationEvents/*.csproj ./Modules/Administration/Administration.IntegrationEvents/
COPY Modules/Administration/Administration.API/*.csproj ./Modules/Administration/Administration.API/

COPY LokalProdusert.Shared/*.csproj ./LokalProdusert.Shared/

COPY ["Directory.Packages.props", "./"]

RUN dotnet restore -v n

# the rest of the source code
COPY . .

# Build the main project
RUN dotnet build "API/LokalProdusert.API/LokalProdusert.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "API/LokalProdusert.API/LokalProdusert.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the environment variable to development
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "LokalProdusert.API.dll"]

