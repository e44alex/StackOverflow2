#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["StackOverflowWebApi/StackOverflowWebApi.csproj", "StackOverflowWebApi/"]
RUN dotnet restore "StackOverflowWebApi/StackOverflowWebApi.csproj"
COPY . .
WORKDIR "/src/StackOverflowWebApi"
RUN dotnet build "StackOverflowWebApi.csproj" -c Release -o /app/build
EXPOSE 80/tcp
# RUN chmod +x ./entrypoint.sh
# CMD /bin/bash ./entrypoint.sh

FROM build AS publish
RUN dotnet publish "StackOverflowWebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StackOverflowWebApi.dll"]