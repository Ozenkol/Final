FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY .. .
RUN dotnet publish "API.csproj" -o /published /p:UseAppHost=false


FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build published/ .
ENTRYPOINT [ "dotnet", "API.dll" ]