FROM docker.io/library/fedora:latest AS build
WORKDIR /source

RUN dnf upgrade -y --refresh; \
    dnf install -y dotnet-sdk-6.0

COPY ./nuget.config /source/
COPY ./src/SmallsOnline.Web.Api/ /source/

RUN dotnet restore
RUN dotnet publish --configuration "Release" --runtime "linux-x64" -p:"PublishReadyToRun=true" --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SmallsOnline.Web.Api.dll"]