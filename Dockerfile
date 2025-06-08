FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["ISF.API/ISF.API.csproj", "ISF.API/"]
COPY ["ISF.Core/ISF.Core.csproj", "ISF.Core/"]
COPY ["ISF.Data/ISF.Data.csproj", "ISF.Data/"]
COPY ["ISF.Services/ISF.Services.csproj", "ISF.Services/"]
RUN dotnet restore "ISF.API/ISF.API.csproj"

# Copy the rest of the files
COPY . .

# Build the application
RUN dotnet build "ISF.API/ISF.API.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ISF.API/ISF.API.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ISF.API.dll"] 