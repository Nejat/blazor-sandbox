#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["blazor-sandbox/blazor-sandbox.csproj", "blazor-sandbox/"]
COPY ["ui-components/ui-components.csproj", "ui-components/"]
COPY ["razor-components/razor-components.csproj", "razor-components/"]
COPY ["data-model/data-model.csproj", "data-model/"]
COPY ["data-service/data-service.csproj", "data-service/"]
COPY ["gRPC-client/gRPC-client.csproj", "gRPC-client/"]
COPY ["toolbox/toolbox.csproj", "toolbox/"]
RUN dotnet restore "blazor-sandbox/blazor-sandbox.csproj"
COPY . .
WORKDIR "/src/blazor-sandbox"
RUN dotnet build "blazor-sandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "blazor-sandbox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blazor-sandbox.dll"]