#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["data-sandbox/data-sandbox.csproj", "data-sandbox/"]
COPY ["data-model/data-model.csproj", "data-model/"]
RUN dotnet restore "data-sandbox/data-sandbox.csproj"
COPY . .
WORKDIR "/src/data-sandbox"
RUN dotnet build "data-sandbox.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "data-sandbox.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "data-sandbox.dll"]