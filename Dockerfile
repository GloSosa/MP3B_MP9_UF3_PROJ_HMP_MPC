# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copiar el proyecto
COPY TenguiNoTengui/*.csproj ./TenguiNoTengui/
WORKDIR /app/TenguiNoTengui

# Restaurar dependencias
RUN dotnet restore

# Copiar el resto del código
COPY . .

# Publicar la app en modo Release
RUN dotnet publish -c Release -o /app/publish

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Exponer el puerto por donde correrá la app
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

# Comando de arranque
ENTRYPOINT ["dotnet", "TenguiNoTengui.dll"]
