FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src
COPY ["TalentoPlus.API/TalentoPlus.API.csproj", "TalentoPlus.API/"]
COPY ["TalentoPlus.Application/TalentoPlus.Application.csproj", "TalentoPlus.Application/"]
COPY ["TalentoPlus.Domain/TalentoPlus.Domain.csproj", "TalentoPlus.Domain/"]
COPY ["TalentoPlus.Infrastructure/TalentoPlus.Infrastructure.csproj", "TalentoPlus.Infrastructure/"]
RUN dotnet restore "TalentoPlus.API/TalentoPlus.API.csproj"
COPY . .
WORKDIR "/src/TalentoPlus.API"
RUN dotnet build "TalentoPlus.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TalentoPlus.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentoPlus.API.dll"]
