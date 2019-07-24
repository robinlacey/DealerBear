FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./DealerBear/*.csproj ./DealerBear/
RUN dotnet restore ./DealerBear

# Copy everything else and build
COPY ./DealerBear/. ./DealerBear/
RUN dotnet publish ./DealerBear -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/DealerBear/out .
ENTRYPOINT ["dotnet", "DealerBear.dll"]