FROM mcr.microsoft.com/dotnet/aspnet:5.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:5.0-focal AS build
WORKDIR /src
COPY ["NotAlone.WebApp/NotAlone.WebApp.csproj", "NotAlone.WebApp/"]
RUN dotnet restore "NotAlone.WebApp/NotAlone.WebApp.csproj"
COPY . .
WORKDIR "/src/NotAlone.WebApp"
RUN dotnet build "NotAlone.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NotAlone.WebApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

ARG VkApiSettings_AccessToken
ARG VkApiSettings_Confirmation

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet NotAlone.WebApp.dll

