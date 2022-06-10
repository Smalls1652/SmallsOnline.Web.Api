FROM docker.io/library/fedora:latest AS build
WORKDIR /source

RUN dnf upgrade -y --refresh; \
    dnf install -y dotnet-sdk-6.0

COPY ./src /source/src
RUN dotnet restore ./src/SmallsOnline.Web.Api
RUN dotnet publish ./src/SmallsOnline.Web.Api --configuration "Release" --runtime "linux-x64" -p:"PublishReadyToRun=true" --output /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SmallsOnline.Web.Api.dll"]