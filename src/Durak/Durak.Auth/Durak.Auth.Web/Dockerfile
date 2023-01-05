#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Durak.Auth/Durak.Auth.Web/Durak.Auth.Web.csproj", "Durak.Auth/Durak.Auth.Web/"]
COPY ["Durak.Auth/Durak.Auth.Infrastructure/Durak.Auth.Infrastructure.csproj", "Durak.Auth/Durak.Auth.Infrastructure/"]
COPY ["Durak.Auth/Durak.Auth.Domain/Durak.Auth.Domain.csproj", "Durak.Auth/Durak.Auth.Domain/"]
RUN dotnet restore "Durak.Auth/Durak.Auth.Web/Durak.Auth.Web.csproj"
COPY . .
WORKDIR "/src/Durak.Auth/Durak.Auth.Web"
RUN dotnet build "Durak.Auth.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Durak.Auth.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Durak.Auth.Web.dll"]