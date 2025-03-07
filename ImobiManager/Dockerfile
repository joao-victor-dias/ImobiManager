FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /app

COPY *.csproj ./

RUN dotnet restore

RUN dotnet tool install --global dotnet-ef

COPY . ./

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/sdk:9.0

WORKDIR /app

COPY --from=build /app/publish .

RUN dotnet tool install --global dotnet-ef

ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["sh", "-c", "dotnet ef database update && dotnet ImobiManager.dll"]