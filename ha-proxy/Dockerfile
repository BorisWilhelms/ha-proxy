FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "ha-proxy.csproj" .
RUN dotnet restore "ha-proxy.csproj"
COPY . .
RUN dotnet build "ha-proxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ha-proxy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ha-proxy.dll"]
