FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DotNetPlayground.Api/DotNetPlayground.Api.Http.Host/DotNetPlayground.Api.Http.Host.csproj", "DotNetPlayground.Api/DotNetPlayground.Api.Http.Host/"]
RUN dotnet restore "DotNetPlayground.Api/DotNetPlayground.Api.Http.Host/DotNetPlayground.Api.Http.Host.csproj"
COPY . .
WORKDIR "/src/DotNetPlayground.Api/DotNetPlayground.Api.Http.Host"
RUN dotnet build "DotNetPlayground.Api.Http.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DotNetPlayground.Api.Http.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DotNetPlayground.Api.Http.Host.dll"]
