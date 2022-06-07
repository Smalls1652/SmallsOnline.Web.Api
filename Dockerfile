FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

COPY SmallsOnline.Web.Api.sln /source/
COPY ./src /source/src
RUN dotnet restore SmallsOnline.Web.Api.sln
RUN dotnet publish SmallsOnline.Web.Api.sln --configuration release --runtime linux-x64 --output /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "SmallsOnline.Web.Api.dll"]