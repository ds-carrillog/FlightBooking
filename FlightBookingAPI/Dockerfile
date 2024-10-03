
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

RUN mkdir -p /app/data && chmod -R 777 /app/data

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FlightBookingAPI.csproj", "."]
RUN dotnet restore "FlightBookingAPI.csproj"
COPY . .
RUN dotnet build "FlightBookingAPI.csproj" -c Release -o /app/build


FROM build AS publish
RUN dotnet publish "FlightBookingAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlightBookingAPI.dll"]
