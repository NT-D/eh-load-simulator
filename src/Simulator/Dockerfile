# Build app
FROM mcr.microsoft.com/dotnet/core/sdk:3.1.404-bionic AS build
WORKDIR /src

COPY *.csproj ./
RUN dotnet restore

COPY . .
RUN dotnet publish -c Release -o out

# Deploy app
FROM mcr.microsoft.com/dotnet/core/runtime:3.1.10-bionic
WORKDIR /app
COPY --from=build /src/out ./
ENTRYPOINT [ "dotnet", "Simulator.dll" ]