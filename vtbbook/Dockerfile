FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_ENVIRONMENT=prod

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["vtbbook/vtbbook.csproj", "vtbbook/"]
COPY ["vtbbook.Core.Common/vtbbook.Core.Common.csproj", "vtbbook.Core.Common/"]
COPY ["vtbbook.Core.DataAccess/vtbbook.Core.DataAccess.csproj", "vtbbook.Core.DataAccess/"]
COPY ["vtbbook.Core.Web/vtbbook.Core.Web.csproj", "vtbbook.Core.Web/"]
RUN dotnet restore "vtbbook/vtbbook.csproj" -r linux-arm64
COPY . .
WORKDIR "/src/vtbbook"
RUN ASPNETCORE_ENVIRONMENT="prod" \
      dotnet build "vtbbook.csproj" -c Release -o /app/build -r linux-musl-arm64

FROM build AS publish
RUN ASPNETCORE_ENVIRONMENT="prod" \
      dotnet publish "vtbbook.csproj" -c Release -o /app/publish -r linux-musl-arm64

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
