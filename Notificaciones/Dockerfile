#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV DOTNET_ENVIRONMENT=""

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Notificaciones/WebUI/WebUI.csproj", "WebUI/"] 
COPY ["Notificaciones/Application/Application.csproj" , "Application/"]
COPY ["Notificaciones/Domain/Domain.csproj", "Domain/"]
COPY ["Notificaciones/Infrastructure/Infrastructure.csproj" , "Infrastructure/"]
RUN dotnet restore "WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/Notificaciones/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Notificaciones.WebUI.dll"]
