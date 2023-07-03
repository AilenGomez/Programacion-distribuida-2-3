#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

ENV SERVER=""
ENV PORT=""
ENV DATABASE=""
ENV USERID=""
ENV PASSWORDDB=""
ENV NOTIFICACIONAPIURL=""
ENV M=""
ENV N=""
ENV DOTNET_ENVIRONMENT=""

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VentaEntrada/WebUI/WebUI.csproj", "WebUI/"] 
COPY ["VentaEntrada/Application/Application.csproj" , "Application/"]
COPY ["VentaEntrada/Domain/Domain.csproj", "Domain/"]
COPY ["VentaEntrada/Infrastructure/Infrastructure.csproj" , "Infrastructure/"]
RUN dotnet restore "WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/VentaEntrada/WebUI"
RUN dotnet build "WebUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VentaEntrada.WebUI.dll"]
