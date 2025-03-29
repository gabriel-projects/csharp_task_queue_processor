# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Api.GRRInnovations.TaskQueue.Processor/Api.GRRInnovations.TaskQueue.Processor.csproj", "Api.GRRInnovations.TaskQueue.Processor/"]
COPY ["Api.GRRInnovations.TaskQueue.Processor.Application/Api.GRRInnovations.TaskQueue.Processor.Application.csproj", "Api.GRRInnovations.TaskQueue.Processor.Application/"]
COPY ["Api.GRRInnovations.TaskQueue.Processor.Infrastructure/Api.GRRInnovations.TaskQueue.Processor.Infrastructure.csproj", "Api.GRRInnovations.TaskQueue.Processor.Infrastructure/"]
COPY ["Api.GRRInnovations.TaskQueue.Processor.Domain/Api.GRRInnovations.TaskQueue.Processor.Domain.csproj", "Api.GRRInnovations.TaskQueue.Processor.Domain/"]
COPY ["Api.GRRInnovations.TaskQueue.Processor.Worker/Api.GRRInnovations.TaskQueue.Processor.Worker.csproj", "Api.GRRInnovations.TaskQueue.Processor.Worker/"]
RUN dotnet restore "./Api.GRRInnovations.TaskQueue.Processor/Api.GRRInnovations.TaskQueue.Processor.csproj"
COPY . .
WORKDIR "/src/Api.GRRInnovations.TaskQueue.Processor"
RUN dotnet build "./Api.GRRInnovations.TaskQueue.Processor.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Api.GRRInnovations.TaskQueue.Processor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.GRRInnovations.TaskQueue.Processor.dll"]