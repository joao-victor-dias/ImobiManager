FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /src

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build_stage
WORKDIR /src

COPY *.csproj ./

RUN dotnet restore

COPY . ./
WORKDIR "/src/ImobiManager"

FROM build_stage as migrations

RUN dotnet tool install --global dotnet-ef --version 9.*
ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet build  /src/ImobiManager.csproj -c Release -o /app/build

RUN dotnet ef migrations add InitialMigration --project /src/ImobiManager.csproj || echo "Migration already exists"
RUN dotnet ef database update

FROM build_stage AS publish
RUN dotnet publish  /src/ImobiManager.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ImobiManager.dll"]