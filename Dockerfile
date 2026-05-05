FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY FrontiersDemo.sln .
COPY src/FrontiersDemo.Api/FrontiersDemo.Api.csproj                         src/FrontiersDemo.Api/
COPY src/FrontiersDemo.Application/FrontiersDemo.Application.csproj         src/FrontiersDemo.Application/
COPY src/FrontiersDemo.Domain/FrontiersDemo.Domain.csproj                   src/FrontiersDemo.Domain/
COPY src/FrontiersDemo.Infrastructure/FrontiersDemo.Infrastructure.csproj   src/FrontiersDemo.Infrastructure/

RUN dotnet restore src/FrontiersDemo.Api/FrontiersDemo.Api.csproj

COPY src/ src/

RUN dotnet publish src/FrontiersDemo.Api/FrontiersDemo.Api.csproj \
    -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "FrontiersDemo.Api.dll"]
