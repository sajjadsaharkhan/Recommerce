FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Recommerce/Recommerce.csproj", "Recommerce/"]
COPY ["Recommerce.Data/Recommerce.Data.csproj", "Recommerce.Data/"]
COPY ["Recommerce.Services/Recommerce.Services.csproj", "Recommerce.Services/"]
COPY ["Recommerce.Infrastructure/Recommerce.Infrastructure.csproj", "Recommerce.Infrastructure/"]

RUN dotnet restore "Recommerce/Recommerce.csproj"
COPY . .
WORKDIR "/src/Recommerce"
RUN dotnet build "Recommerce.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Recommerce.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
ENV TZ=Asia/Tehran
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Recommerce.dll"]