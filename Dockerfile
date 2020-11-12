FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

# Run dotnet restore with only necessary files for Docker caching
COPY *.sln ./
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore TrueLayerAssignment.sln

# Copy remaining files
COPY . .

WORKDIR /app/TrueLayerAssignment.Web

# Publish project
RUN dotnet publish TrueLayerAssignment.Web.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime

WORKDIR /app
COPY --from=build /app/TrueLayerAssignment.Web/out ./

ENTRYPOINT ["dotnet", "TrueLayerAssignment.Web.dll"]
EXPOSE 80