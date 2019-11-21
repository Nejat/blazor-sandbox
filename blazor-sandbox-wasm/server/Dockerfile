#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["blazor-sandbox-wasm/server/blazor-sandbox-wasm-server.csproj", "blazor-sandbox-wasm/server/"]
COPY ["blazor-sandbox-wasm/client/blazor-sandbox-wasm-client.csproj", "blazor-sandbox-wasm/client/"]
COPY ["ui-components/ui-components.csproj", "ui-components/"]
COPY ["razor-components/razor-components-wasm.csproj", "razor-components/"]
COPY ["data-model/data-model.csproj", "data-model/"]
COPY ["data-service/data-service.csproj", "data-service/"]
COPY ["toolbox/toolbox.csproj", "toolbox/"]
RUN dotnet restore "blazor-sandbox-wasm/server/blazor-sandbox-wasm-server.csproj"
COPY . .
WORKDIR "/src/blazor-sandbox-wasm/server"
RUN dotnet build "blazor-sandbox-wasm-server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "blazor-sandbox-wasm-server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blazor-sandbox-wasm-server.dll"]