FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["SophosSolutions.Overtimes.WebAPI/SophosSolutions.Overtimes.WebAPI.csproj", "SophosSolutions.Overtimes.WebAPI/"]
COPY ["SophosSolutions.Overtimes.Application/SophosSolutions.Overtimes.Application.csproj", "SophosSolutions.Overtimes.Application/"]
COPY ["SophosSolutions.Overtimes.Models/SophosSolutions.Overtimes.Models.csproj", "SophosSolutions.Overtimes.Models/"]
COPY ["SophosSolutions.Overtimes.Infrastructure/SophosSolutions.Overtimes.Infrastructure.csproj", "SophosSolutions.Overtimes.Infrastructure/"]
RUN dotnet restore "SophosSolutions.Overtimes.WebAPI/SophosSolutions.Overtimes.WebAPI.csproj"
COPY . .
WORKDIR "/src/SophosSolutions.Overtimes.WebAPI"
RUN dotnet build "SophosSolutions.Overtimes.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SophosSolutions.Overtimes.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SophosSolutions.Overtimes.WebAPI.dll"]

#docker build . -t sophos-solutions-overtimes-backend-image:1.0.0
#docker run -d --name sophos-solutions-overtimes-backend-app -p 8089:443 sophos-solutions-overtimes-backend-image:1.0.0