# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files first for better layer caching
COPY P10_WebApi.sln ./
COPY P10_WebApi.csproj ./
RUN dotnet restore ./P10_WebApi.csproj

# Copy the rest of the source and publish
COPY . .
RUN dotnet publish P10_WebApi.csproj -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Render provides PORT; bind to 0.0.0.0 and respect PORT
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT}
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

# Copy published build
COPY --from=build /app/publish ./

# Expose default port (Render will still inject PORT)
EXPOSE 8080

# Run
CMD ["dotnet", "P10_WebApi.dll"]