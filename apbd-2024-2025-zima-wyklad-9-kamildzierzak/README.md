# Ćwiczenia 9

## How to check if functionallity works?

1. (1) There is more than 10 records so it should be at least 2 pages or we can change pageSize.
1. (2) We can delete clients with id 18-42.
1. (3) We can send POST request to assign client to the trip, but to be honest i found multiple illogical requirements in this task ;)

## How to connect to SQL Server Docker Container through SSMS

1. Server type: Database Engine
1. Server name: localhost, 1433
1. Authentication: SQL Server Authentication
   1. Login: sa
   1. Password: Strongpassword314!
1. Probably u need to click Trust server certificate

## Database first stuff

1. Packages:
   - Microsoft.EntityFrameworkCore
   - Microsoft.EntityFrameworkCore.SqlServer
   - Microsoft.EntityFrameworkCore.Design
   - Microsoft.AspNetCore.Mvc.NewtonsoftJson
1. Commands:
   - `dotnet new tool-manifest`
   - `dotnet tool install dotnet-ef --version 8.0.0`
1. Add to .csproj:
   - [`<InvariantGlobalization>false</InvariantGlobalization>`](https://learn.microsoft.com/en-us/dotnet/core/runtime-config/globalization)
1. Add connection string to app settings:

```
// Something like that
"ConnectionStrings": {
    "DefaultConnection": "Server=exercise9.sqlserver, 1433; Database=testdb; User Id=sa; Password=strongpassword314!; TrustServerCertificate=True;"
  },
```

1. Create directories Models and Context
1. Run the command:

```
dotnet ef dbcontext scaffold "Data Source=localhost, 1433;Initial Catalog=testdb;User Id=sa;Password=strongpassword314!;Encrypt=False;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models --context-dir Context
```
