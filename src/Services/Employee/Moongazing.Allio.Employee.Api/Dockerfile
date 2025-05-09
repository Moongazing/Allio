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
COPY ["src/Services/Employee/Moongazing.Allio.Employee.Api/Moongazing.Allio.Employee.Api.csproj", "src/Services/Employee/Moongazing.Allio.Employee.Api/"]
COPY ["src/Kernel/Moongazing.Kernel.CrossCuttingConcerns/Moongazing.Kernel.CrossCuttingConcerns.csproj", "src/Kernel/Moongazing.Kernel.CrossCuttingConcerns/"]
COPY ["src/Kernel/Moongazing.Kernel.Mailing/Moongazing.Kernel.Mailing.csproj", "src/Kernel/Moongazing.Kernel.Mailing/"]
COPY ["src/Kernel/Moongazing.Kernel.Persistence/Moongazing.Kernel.Persistence.csproj", "src/Kernel/Moongazing.Kernel.Persistence/"]
COPY ["src/Kernel/Moongazing.Kernel.Shared/Moongazing.Kernel.Shared.csproj", "src/Kernel/Moongazing.Kernel.Shared/"]
COPY ["src/Services/Employee/Moongazing.Allio.Employee.Application/Moongazing.Allio.Employee.Application.csproj", "src/Services/Employee/Moongazing.Allio.Employee.Application/"]
COPY ["src/Kernel/Moongazing.Kernel.Application/Moongazing.Kernel.Application.csproj", "src/Kernel/Moongazing.Kernel.Application/"]
COPY ["src/Kernel/Moongazing.Kernel.Messaging/Moongazing.Kernel.Messaging.csproj", "src/Kernel/Moongazing.Kernel.Messaging/"]
COPY ["src/Services/Employee/Moongazing.Allio.Employee.Domain/Moongazing.Allio.Employee.Domain.csproj", "src/Services/Employee/Moongazing.Allio.Employee.Domain/"]
COPY ["src/Services/Employee/Moongazing.Allio.Employee.Persistence/Moongazing.Allio.Employee.Persistence.csproj", "src/Services/Employee/Moongazing.Allio.Employee.Persistence/"]
RUN dotnet restore "./src/Services/Employee/Moongazing.Allio.Employee.Api/Moongazing.Allio.Employee.Api.csproj"
COPY . .
WORKDIR "/src/src/Services/Employee/Moongazing.Allio.Employee.Api"
RUN dotnet build "./Moongazing.Allio.Employee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Moongazing.Allio.Employee.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Moongazing.Allio.Employee.Api.dll"]