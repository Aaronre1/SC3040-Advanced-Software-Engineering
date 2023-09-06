# SC3040-Advanced-Software-Engineering

# Setup

## Add a migration
```
dotnet ef migrations add migrationName --project src/Infrastructure --startup-project src/Web --context SqliteApplicationDbContext --output-dir Data/Migrations/SqliteMigrations -- --provider Sqlite
dotnet ef migrations add migrationName --project src/Infrastructure --startup-project src/Web --context ApplicationDbContext --output-dir Data/Migrations/SqlServerMigrations -- --provider SqlServer
```
## Apply migration
Running the app will apply the migration automatically through `ApplicationDbContextInitialiser.cs`.
