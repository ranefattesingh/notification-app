FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

ENV ASPNETCORE_ENVIRONMENT=production
ENV NOTIFICATION_UI_HOST="http://0.0.0.0:8080"

EXPOSE 8080
EXPOSE 8081

ENTRYPOINT ["dotnet", "NotificationApi.dll" ]