FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
ARG EVENTHUB_NUGET_FEED=http://host.docker.internal:5555/v3/index.json
ENV EVENTHUB_NUGET_FEED=${EVENTHUB_NUGET_FEED}
WORKDIR /src
COPY ["Directory.Build.props", "Directory.Packages.props", "nuget.config", "./"]
COPY ["src/EventHub.WebUi/EventHub.WebUi.csproj", "src/EventHub.WebUi/"]
RUN dotnet restore "src/EventHub.WebUi/EventHub.WebUi.csproj"
COPY . .
RUN dotnet publish "src/EventHub.WebUi/EventHub.WebUi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine AS runtime
RUN apk add --no-cache icu-libs wget && \
    adduser -D -h /app appuser
WORKDIR /app
COPY --from=build /app/publish .
USER appuser
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
HEALTHCHECK --interval=15s --timeout=3s --start-period=10s --retries=3 \
    CMD wget --no-verbose --tries=1 --spider http://localhost:8080/healthz || exit 1
ENTRYPOINT ["dotnet", "EventHub.WebUi.dll"]
