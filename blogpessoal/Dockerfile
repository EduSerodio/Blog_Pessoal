FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV ASPNETCORE_URLS http://*:80
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["blogpessoal.csproj", "."]
RUN dotnet restore "./blogpessoal.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "blogpessoal.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "blogpessoal.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "blogpessoal.dll"]
