FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["grpc-sandbox/grpc-sandbox.csproj", "grpc-sandbox/"]
RUN dotnet restore "grpc-sandbox/grpc-sandbox.csproj"
COPY . .
WORKDIR "/src/grpc-sandbox"
RUN dotnet build "grpc-sandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "grpc-sandbox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "grpc-sandbox.dll"]
