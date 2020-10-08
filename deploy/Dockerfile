FROM mcr.microsoft.com/dotnet/core/aspnet:3.1.3 AS base

WORKDIR /app

ENV ASPNETCORE_URLS="http://*:5000"
ENV ASPNETCORE_ENVIRONMENT="server.talrace.com"
HEALTHCHECK CMD curl --fail http://localhost:5000/ || exit 1

FROM mcr.microsoft.com/dotnet/core/sdk:3.1.300 AS build
WORKDIR /build 

COPY . .

# #build to allow for ts models
WORKDIR /build/src/Server.Dtos/
RUN dotnet build

# #create ts models
WORKDIR /build/utils/Server.ModelsGenerator/
RUN dotnet run ts

# #setup node.js
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_10.x | bash - && \
    apt-get install -y build-essential nodejs

WORKDIR /build/ui
RUN npm install
RUN npm run deploy

# #setup app
WORKDIR /build/src/Server.Application/
RUN dotnet publish -c Release -o /app

FROM base AS final

COPY --from=build /app .
COPY --from=build /build/src/Server.Application/wwwroot /app/wwwroot/

ENTRYPOINT ["dotnet", "./Server.Application.dll"]