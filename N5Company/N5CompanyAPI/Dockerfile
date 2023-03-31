#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["N5CompanyAPI/N5CompanyAPI.csproj", "N5CompanyAPI/"]
COPY ["N5Company.Business/N5Company.Business.csproj", "N5Company.Business/"]
COPY ["N5Company.Commands/N5Company.Commands.csproj", "N5Company.Commands/"]
COPY ["N5Company.Entities/N5Company.Entities.csproj", "N5Company.Entities/"]
COPY ["N5Company.Repositories/N5Company.Repositories.csproj", "N5Company.Repositories/"]
COPY ["N5Company.Queries/N5Company.Queries.csproj", "N5Company.Queries/"]
COPY ["N5Company.MapperProfiles/N5Company.MapperProfiles.csproj", "N5Company.MapperProfiles/"]
RUN dotnet restore "N5CompanyAPI/N5CompanyAPI.csproj"
COPY . .
WORKDIR "/src/N5CompanyAPI"
RUN dotnet build "N5CompanyAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5CompanyAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5CompanyAPI.dll"]