#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/new-job-challenge.carrefour.api/new-job-challenge.carrefour.api.csproj", "src/new-job-challenge.carrefour.api/"]
RUN dotnet restore "src/new-job-challenge.carrefour.api/new-job-challenge.carrefour.api.csproj"
COPY . .
WORKDIR "/src/src/new-job-challenge.carrefour.api"
RUN dotnet build "new-job-challenge.carrefour.api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "new-job-challenge.carrefour.api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "new-job-challenge.carrefour.api.dll"]