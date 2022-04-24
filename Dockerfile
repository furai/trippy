# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY TrippyWeb/*.csproj ./TrippyWeb/
RUN dotnet restore -r linux-x64 /p:PublishReadyToRun=true

# copy everything else and build app
COPY TrippyWeb/. ./TrippyWeb/
WORKDIR /source/TrippyWeb
RUN dotnet publish -c release -o /app -r linux-x64 --self-contained true --no-restore /p:PublishTrimmed=true /p:PublishReadyToRun=true /p:PublishSingleFile=true

# final stage/image
FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-bullseye-slim-amd64
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["./TrippyWeb"]