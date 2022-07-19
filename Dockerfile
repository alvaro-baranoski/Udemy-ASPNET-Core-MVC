FROM mcr.microsoft.com/dotnet/sdk:6.0 as BUILD
WORKDIR /app 

#
# copy csproj and restore as distinct layers
COPY *.sln .
COPY BulkyBook.DataAccess/*.csproj ./BulkyBook.DataAccess/
COPY BulkyBook.Models/*.csproj ./BulkyBook.Models/
COPY BulkyBook.Utility/*.csproj ./BulkyBook.Utility/
COPY BulkyBookWeb/*.csproj ./BulkyBookWeb/

#
RUN dotnet restore

#
# copy everything else and build app
COPY BulkyBook.DataAccess/. ./BulkyBook.DataAccess/
COPY BulkyBook.Models/. ./BulkyBook.Models/
COPY BulkyBook.Utility/. ./BulkyBook.Utility/
COPY BulkyBookWeb/. ./BulkyBookWeb/

#
WORKDIR /app/BulkyBookWeb
RUN rm appsettings.json
RUN mv appsettings.Docker.json appsettings.json

#
RUN dotnet publish -c Release -o out

#
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS RUNTIME
WORKDIR /app 
COPY --from=BUILD /app/BulkyBookWeb/out ./
ENTRYPOINT ["dotnet", "BulkyBookWeb.dll"]