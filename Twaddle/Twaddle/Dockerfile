FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Twaddle/Twaddle/Twaddle.csproj", "Twaddle/"]
RUN dotnet restore "Twaddle/Twaddle/Twaddle.csproj"
COPY . .
WORKDIR "/src/Twaddle"
RUN dotnet build "Twaddle.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Twaddle.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Twaddle.dll"]
