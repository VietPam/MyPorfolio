FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM --platform=linux/amd64 mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MyPorfolio/MyPorfolio.csproj", "MyPorfolio/"]
RUN dotnet restore "./MyPorfolio/./MyPorfolio.csproj"
COPY . .
WORKDIR "/src/MyPorfolio"
RUN dotnet build "./MyPorfolio.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MyPorfolio.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Copy the SQLite database file to the publish folder
COPY MyPorfolio/Models/mydb.db /app/publish/Models/
RUN chmod -R 777 /app/publish/Models
RUN chmod -R 777 /app/publish/wwwroot

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MyPorfolio.dll"]