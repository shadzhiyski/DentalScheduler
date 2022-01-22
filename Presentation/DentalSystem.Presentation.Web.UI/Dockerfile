FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/Presentation/DentalSystem.Presentation.Web.UI
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build as publish
RUN dotnet publish -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY Presentation/DentalSystem.Presentation.Web.UI/nginx.conf /etc/nginx/nginx.conf