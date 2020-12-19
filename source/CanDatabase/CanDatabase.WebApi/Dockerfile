FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS dotnet-runtime

# .NET App
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS dotnet-builder

WORKDIR /source/CanDatabase

# Restore .NET App
COPY ["CanDatabase/CanDatabase.Application/CanDatabase.Application.csproj", "CanDatabase.Application/"]
COPY ["CanDatabase/CanDatabase.Common/CanDatabase.Common.csproj", "CanDatabase.Common/"]
COPY ["CanDatabase/CanDatabase.Domain/CanDatabase.Domain.csproj", "CanDatabase.Domain/"]
COPY ["CanDatabase/CanDatabase.Persistence/CanDatabase.Persistence.csproj", "CanDatabase.Persistence/"]
COPY ["CanDatabase/CanDatabase.Client/CanDatabase.Client.csproj", "CanDatabase.Client/"]
COPY ["CanDatabase/CanDatabase.Shared/CanDatabase.Shared.csproj", "CanDatabase.Shared/"]
COPY ["CanDatabase/CanDatabase.WebApi/CanDatabase.WebApi.csproj", "CanDatabase.WebApi/"]
RUN dotnet restore "CanDatabase.WebApi/CanDatabase.WebApi.csproj"

# Build .NET App
COPY CanDatabase/ .
WORKDIR /source/CanDatabase/CanDatabase.WebApi
RUN dotnet build "CanDatabase.WebApi.csproj" -c Release -o /app/build

# Publish .NET App
FROM dotnet-builder AS dotnet-publisher
RUN dotnet publish "CanDatabase.WebApi.csproj" -c Release -o /app/publish

# Copy .NET Application From client-builder Image Into final Image
FROM dotnet-runtime AS final
WORKDIR /app
COPY --from=dotnet-publisher /app/publish /app

ENTRYPOINT ["dotnet", "CanDatabase.WebApi.dll"]
