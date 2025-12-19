FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY . .

RUN dotnet restore

WORKDIR /app/Desafio\ SIEG
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build ["/app/Desafio SIEG/out", "."]

EXPOSE 8080
ENTRYPOINT ["dotnet", "Desafio SIEG.dll"]
