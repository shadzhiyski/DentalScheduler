FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5500
EXPOSE 5500

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Presentation/DentalSystem.Presentation.Web.Api
RUN dotnet restore

FROM build as publish
RUN dotnet publish -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DentalSystem.Presentation.Web.Api.dll"]