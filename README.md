# SC3040-Advanced-Software-Engineering

# Setup
## Install .NET Core CLI tools
```
dotnet tool install --global dotnet-ef
```

## Add a migration
```
dotnet ef migrations add migrationName --project src/Infrastructure --startup-project src/Web --context SqliteApplicationDbContext --output-dir Data/Migrations/SqliteMigrations -- --provider Sqlite
```
```
dotnet ef migrations add migrationName --project src/Infrastructure --startup-project src/Web --context ApplicationDbContext --output-dir Data/Migrations/SqlServerMigrations -- --provider SqlServer
```
## Apply migration
Running the app will apply the migration automatically through `ApplicationDbContextInitialiser.cs`.


dotnet ef migrations add itinerary --project src/Infrastructure --startup-project src/Web --context SqliteApplicationDbContext --output-dir Data/Migrations/SqliteMigrations -- --provider Sqlite
dotnet ef migrations add itinerary --project src/Infrastructure --startup-project src/Web --context ApplicationDbContext --output-dir Data/Migrations/SqlServerMigrations -- --provider SqlServer

dotnet ef migrations add money --project src/Infrastructure --startup-project src/Web --context ApplicationDbContext --output-dir Data/Migrations/SqlServerMigrations -- --provider SqlServer

dotnet ef migrations add relations --project src/Infrastructure --startup-project src/Web --context SqliteApplicationDbContext --output-dir Data/Migrations/SqliteMigrations -- --provider Sqlite
dotnet ef migrations add relations --project src/Infrastructure --startup-project src/Web --context ApplicationDbContext --output-dir Data/Migrations/SqlServerMigrations -- --provider SqlServer