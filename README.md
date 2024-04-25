// Web API using .Net Core 7 and Clean Architecture.


// EF Core Migration

    • Add Migration: dotnet ef migrations add InitialMigration --project src/External/Infrastructure --startup-project src/Web/WebAPI --output-dir Data/Migrations --context ApplicationDbContext

    • Update Migration: dotnet ef database update --project src/External/Infrastructure --startup-project src/Web/WebAPI --context ApplicationDbContext


// Docker

    • git clone <repo url> // if testing on PWD
        
        - cd <repo dir>
        

    • docker build -t <tag> .
    
    • docker run -d -p 80:80 <tag>

// TODO

    • Develop front-end using Angular 17 (In progress)
        - Sign in page (Done)
        - Sign up page (Done)
        - Shopping page

    • JWT Authentication (Done)
        - Refresh token (In progress)
