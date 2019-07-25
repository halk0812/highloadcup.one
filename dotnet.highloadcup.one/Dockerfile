FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["dotnet.highloadcup.one/dotnet.highloadcup.one.csproj", "dotnet.highloadcup.one/"]
RUN dotnet restore "dotnet.highloadcup.one/dotnet.highloadcup.one.csproj"
COPY . .
WORKDIR "/src/dotnet.highloadcup.one"
RUN dotnet build "dotnet.highloadcup.one.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "dotnet.highloadcup.one.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "dotnet.highloadcup.one.dll"]