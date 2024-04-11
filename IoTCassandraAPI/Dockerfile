FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "IoTCassandraAPI.sln"
WORKDIR "/src"
RUN dotnet build "IoTCassandraAPI.sln" -c Release -o /app

FROM build AS publish
RUN dotnet publish "IoTCassandraAPI.sln" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "IoTCassandraAPI.dll"]