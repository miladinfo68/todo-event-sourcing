#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Todo.Write/Write.API/Write.API.csproj", "src/Todo.Write/Write.API/"]
COPY ["src/Todo.Write/Write.Infrastructure/Write.Infrastructure.csproj", "src/Todo.Write/Write.Infrastructure/"]
COPY ["src/core/Core.Infrastructure/Core.Infrastructure.csproj", "src/core/Core.Infrastructure/"]
COPY ["src/core/Core.Application/Core.Application.csproj", "src/core/Core.Application/"]
COPY ["src/core/Core.Domain/Core.Domain.csproj", "src/core/Core.Domain/"]
COPY ["src/core/Messages/Messages.csproj", "src/core/Messages/"]
COPY ["src/Todo.Write/Write.Application/Write.Application.csproj", "src/Todo.Write/Write.Application/"]
COPY ["src/Todo.Write/Write.Domain/Write.Domain.csproj", "src/Todo.Write/Write.Domain/"]
RUN dotnet restore "src/Todo.Write/Write.API/Write.API.csproj"
COPY . .
WORKDIR "/src/src/Todo.Write/Write.API"
RUN dotnet build "Write.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Write.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Write.API.dll"]