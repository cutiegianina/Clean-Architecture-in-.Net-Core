// Web API using .Net Core 7 and Clean Architecture.


// EF Core Migration

    • Add Migration: dotnet ef migrations add InitialMigration --project src/Infrastructure --startup-project src/WebAPI --output-dir Migrations --context ApplicationDbContext

    • Update Migration: dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI --context ApplicationDbContext