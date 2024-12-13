FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY *.csproj ./
RUN dotnet restore

COPY . ./

RUN dotnet publish -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /out .

EXPOSE 80
EXPOSE 443
EXPOSE 7199
EXPOSE 5247

ENTRYPOINT ["dotnet", "EcomPortal.dll"]
