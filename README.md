# SC3040-Advanced-Software-Engineering

# Setup

## Add a migration
Only use SqlServer when applying migrations
```
dotnet ef migrations add initial --project src/Infrastructure --startup-project src/Web --output-dir Data/Migrations
```
Rename `initial` to your own migration name.
```
dotnet ef migrations add addToDoList --project src/Infrastructure --startup-project src/Web --output-dir Data/Migrations
```

## Update database schema
Run the app, it should apply automatically.