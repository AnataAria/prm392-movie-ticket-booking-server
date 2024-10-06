FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /api
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /appbuild

COPY . .
RUN dotnet restore
WORKDIR /appbuild/MovieTicketBookingAPI
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /api
COPY --from=build /appbuild/MovieTicketBookingAPI/out .

ENTRYPOINT ["dotnet", "MovieTicketBookingAPI.dll"]