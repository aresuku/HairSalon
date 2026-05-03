FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG CONFIGURATION=Release
WORKDIR /src
COPY ["pr_4.4.csproj", "./"]
RUN dotnet restore "pr_4.4.csproj"
COPY . .
RUN dotnet build "pr_4.4.csproj" -c $CONFIGURATION -o /app/build

FROM build AS publish
ARG CONFIGURATION=Release
RUN dotnet publish "pr_4.4.csproj" -c $CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .

RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "pr_4.4.dll"]