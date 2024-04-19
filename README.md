// Web API using .Net Core 7 and Clean Architecture.


// EF Core Migration

    • Add Migration: dotnet ef migrations add InitialMigration --project src/Infrastructure --startup-project src/WebAPI --output-dir Data/Migrations --context ApplicationDbContext

    • Update Migration: dotnet ef database update --project src/Infrastructure --startup-project src/WebAPI --context ApplicationDbContext


// Docker

    • git clone <repo url> // if testing on PWD
        
        - cd <repo dir>
        

    • docker build -t <tag> .
    
    • docker run -d -p 80:80 <tag>

// TODO

    • Develop front-end using Angular 17 (In progress)
        - Sign in page (Partially done)
        - Sign up page
        - Shopping page

    • JWT Authentication