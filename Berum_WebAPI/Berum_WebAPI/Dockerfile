# --- Compilación ---
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["Berum_WebAPI.csproj", "./"]
RUN dotnet restore "Berum_WebAPI.csproj"

COPY . .
RUN dotnet publish "Berum_WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# --- Ejecución ---
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Berum_WebAPI.dll"]