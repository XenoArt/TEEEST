# Use the official .NET image as a build stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TEEEST/TEEEST.csproj", "TEEEST/"]
RUN dotnet restore "TEEEST/TEEEST.csproj"
COPY . .
WORKDIR "/src/TEEEST"
RUN dotnet build "TEEEST.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TEEEST.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TEEEST.dll"]