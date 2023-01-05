#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Durak.Game/Durak.Game.Web/Durak.Game.Web.csproj", "Durak.Game/Durak.Game.Web/"]
COPY ["Durak.Game/Durak.Game.Infrastructure/Durak.Game.Infrastructure.csproj", "Durak.Game/Durak.Game.Infrastructure/"]
COPY ["Durak.Game/Durak.Game.Domain/Durak.Game.Domain.csproj", "Durak.Game/Durak.Game.Domain/"]
RUN dotnet restore "Durak.Game/Durak.Game.Web/Durak.Game.Web.csproj"
COPY . .
WORKDIR "/src/Durak.Game/Durak.Game.Web"
RUN dotnet build "Durak.Game.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Durak.Game.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Durak.Game.Web.dll"]