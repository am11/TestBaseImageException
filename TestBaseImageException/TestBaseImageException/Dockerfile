#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.13-alpine3.17-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestBaseImageException/TestBaseImageException.csproj", "TestBaseImageException/"]
RUN dotnet restore "TestBaseImageException/TestBaseImageException.csproj"
COPY . .
WORKDIR "/src/TestBaseImageException"
RUN dotnet build "TestBaseImageException.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestBaseImageException.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestBaseImageException.dll"]