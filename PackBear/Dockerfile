FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./*.csproj ./PackBear/
RUN dotnet restore ./PackBear

# Copy everything else and build
COPY ./. ./PackBear/
RUN dotnet publish ./PackBear -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY --from=build-env /app/PackBear/out .
ENTRYPOINT ["dotnet", "PackBear.dll"]