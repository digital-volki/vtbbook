#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["vtbbook/vtbbook.csproj", "vtbbook/"]
COPY ["vtbbook.Core.DataAccess/vtbbook.Core.DataAccess.csproj", "vtbbook.Core.DataAccess/"]
COPY ["vtbbook.Core.Common/vtbbook.Core.Common.csproj", "vtbbook.Core.Common/"]
COPY ["vtbbook.Application.Service/vtbbook.Application.Service.csproj", "vtbbook.Application.Service/"]
COPY ["vtbbook.Application.Domain/vtbbook.Application.Domain.csproj", "vtbbook.Application.Domain/"]
COPY ["vtbbook.Application.Coupons/vtbbook.Application.Coupons.csproj", "vtbbook.Application.Coupons/"]
RUN dotnet restore "vtbbook/vtbbook.csproj"
COPY . .
WORKDIR "/src/vtbbook"
RUN ASPNETCORE_ENVIRONMENT="docker" \
	dotnet build "vtbbook.csproj" -c Release -o /app/build

FROM build AS publish
RUN ASPNETCORE_ENVIRONMENT="docker" \ 
	dotnet publish "vtbbook.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "vtbbook.dll"]