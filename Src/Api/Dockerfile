#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Chamados.Service.Api/Chamados.Service.Api.csproj", "Chamados.Service.Api/"]
RUN dotnet restore "Chamados.Service.Api/Chamados.Service.Api.csproj"
COPY . .
WORKDIR "/src/Chamados.Service.Api"
RUN dotnet build "Chamados.Service.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Chamados.Service.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Chamados.Service.Api.dll"]