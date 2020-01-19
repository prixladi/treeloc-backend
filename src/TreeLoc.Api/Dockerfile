#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src

COPY src/TreeLoc.Api/TreeLoc.Api.csproj TreeLoc.Api/
COPY src/TreeLoc.Core/TreeLoc.Core.csproj TreeLoc.Core/
COPY src/TreeLoc.Database/TreeLoc.Database.csproj TreeLoc.Database/
RUN dotnet restore TreeLoc.Api/TreeLoc.Api.csproj

COPY src/TreeLoc.Api/ TreeLoc.Api/
COPY src/TreeLoc.Core/ TreeLoc.Core/
COPY src/TreeLoc.Database/ TreeLoc.Database/
WORKDIR /src/TreeLoc.Api
RUN dotnet build TreeLoc.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish TreeLoc.Api.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TreeLoc.Api.dll"]