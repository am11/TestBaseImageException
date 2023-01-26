#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0.13-alpine3.17-amd64 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine3.17-amd64 AS build
WORKDIR /src
COPY ["TestBaseImageException/TestBaseImageException.csproj", "TestBaseImageException/"]
RUN dotnet restore "TestBaseImageException/TestBaseImageException.csproj"
COPY . .
WORKDIR "/src/TestBaseImageException"
RUN dotnet build "TestBaseImageException.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestBaseImageException.csproj" -c Release -o /app/publish /p:UseAppHost=false
WORKDIR /app/publish
RUN dotnet tool install -g dotnet-sos && dotnet tool install -g dotnet-symbol
ENV PATH=${PATH}:/root/.dotnet/tools
RUN apk add lldb py3-lldb file
RUN find /usr/share/dotnet -type f -exec file {} + | grep ELF | while IFS=: read path rest; do dotnet-symbol --timeout 20 "$path"; done
RUN dotnet-sos install
