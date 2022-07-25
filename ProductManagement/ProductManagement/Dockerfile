#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ProductManagement/ProductManagement.csproj", "ProductManagement/"]
RUN dotnet restore "ProductManagement/ProductManagement.csproj"
COPY . .
WORKDIR "/src/ProductManagement"
RUN dotnet build "ProductManagement.csproj" -c Release -o /app/build --network=host

FROM build AS publish
RUN dotnet publish "ProductManagement.csproj" -c Release -o /app/publish --network=host

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProductManagement.dll"]