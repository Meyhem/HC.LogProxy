FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HC.LogProxy.Api/HC.LogProxy.Api.csproj", "HC.LogProxy.Api/"]
RUN dotnet restore "HC.LogProxy.Api/HC.LogProxy.Api.csproj"
COPY . .
WORKDIR "/src/HC.LogProxy.Api"
RUN dotnet build "HC.LogProxy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HC.LogProxy.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HC.LogProxy.Api.dll"]
