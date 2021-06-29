FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["signalr-sandbox/signalr-sandbox.csproj", "signalr-sandbox/"]
RUN dotnet restore "signalr-sandbox/signalr-sandbox.csproj"
COPY . .
WORKDIR "/src/signalr-sandbox"
RUN dotnet build "signalr-sandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "signalr-sandbox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "signalr-sandbox.dll"]
