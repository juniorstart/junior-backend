FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore JuniorStart.csproj
COPY . ./
RUN dotnet publish JuniorStart.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out .

CMD ASPNETCORE_URLS=http://*:$PORT dotnet JuniorStart.dll

