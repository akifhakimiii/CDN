# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution file
COPY *.sln .

# Copy all project files
COPY FreelanceApp/*.csproj ./FreelanceApp/
COPY FreelanceApp.Tests/*.csproj ./FreelanceApp.Tests/

# Restore dependencies
RUN dotnet restore

# Copy the rest of the application code
COPY . .

# Build the project
RUN dotnet build --no-restore -c Release

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish --no-build -c Release -o /app/publish

# Stage 3: Build the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FreelanceApp.dll"]
