# Nova migração de base de dados
dotnet ef migrations add extendIdentityUser --project BulkyBook.DataAccess/BulkyBook.DataAccess.csproj --startup-project BulkyBookWeb/BulkyBookWeb.csproj

# Update da base de dados
dotnet ef database update --project BulkyBook.DataAccess/BulkyBook.DataAccess.csproj --startup-project BulkyBookWeb/BulkyBookWeb.csproj

# Run project
dotnet run --project BulkyBookWeb/BulkyBookWeb.csproj