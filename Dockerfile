FROM docker.io/library/fedora:latest AS build
WORKDIR /source

RUN dnf upgrade -y --refresh; \
    dnf install -y dotnet-sdk-6.0

COPY ./src /source/src
RUN dotnet restore ./src/SmallsOnline.Web.Api
RUN dotnet publish ./src/SmallsOnline.Web.Api --configuration "Release" --runtime "linux-x64" -p:"PublishReadyToRun=true" --output /app

FROM docker.io/rockylinux/rockylinux:latest
WORKDIR /app

RUN dnf upgrade -y --refresh; \
    dnf install -y aspnetcore-runtime-6.0; \
    dnf autoremove -y; \
    dnf clean all

COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SmallsOnline.Web.Api.dll"]