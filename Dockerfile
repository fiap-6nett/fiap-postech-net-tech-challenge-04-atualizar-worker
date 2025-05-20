# Base image for the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8082

# Build image to compile the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the solution file
COPY Contato.Atualizar.Worker.sln .

# Copy the necessary project files
COPY Contato.Atualizar.Worker.Service/Contato.Atualizar.Worker.Service.csproj Contato.Atualizar.Worker.Service/
COPY Contato.Atualizar.Worker.Application/Contato.Atualizar.Worker.Application.csproj Contato.Atualizar.Worker.Application/
COPY Contato.Atualizar.Worker.Domain/Contato.Atualizar.Worker.Domain.csproj Contato.Atualizar.Worker.Domain/
COPY Contato.Atualizar.Worker.Infra/Contato.Atualizar.Worker.Infra.csproj Contato.Atualizar.Worker.Infra/

# Restore the NuGet packages
RUN dotnet restore Contato.Atualizar.Worker.sln

# Copy the rest of the files
COPY . .

# Build the project
WORKDIR /src/Contato.Atualizar.Worker.Service
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build

# Publish the application for deployment
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image that includes the runtime and the compiled application
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set the entrypoint for the Worker application (make sure this is correct)
ENTRYPOINT ["dotnet", "Contato.Atualizar.Worker.Service.dll"]
