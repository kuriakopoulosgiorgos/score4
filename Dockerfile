FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY Application/Application.csproj ./Application/
COPY Domain/Domain.csproj ./Domain/
COPY GrainInterfaces/GrainInterfaces.csproj ./GrainInterfaces/
COPY Grains/Grains.csproj ./Grains/
COPY Web/Web.csproj ./Web/

RUN dotnet restore Web/Web.csproj

COPY Application ./Application
COPY Domain ./Domain
COPY GrainInterfaces ./GrainInterfaces
COPY Grains ./Grains
COPY Web ./Web

RUN dotnet publish Web/Web.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

RUN adduser --disabled-password --home /app appuser
USER appuser

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=5s --start-period=10s --retries=3 \
  CMD wget -qO- http://localhost:8080/health || exit 1

EXPOSE 8080

ENTRYPOINT ["dotnet", "Web.dll"]
