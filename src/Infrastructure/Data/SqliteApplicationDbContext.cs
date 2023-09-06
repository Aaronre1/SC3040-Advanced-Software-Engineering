using Microsoft.EntityFrameworkCore;

namespace ASE3040.Infrastructure.Data;

public class SqliteApplicationDbContext : ApplicationDbContext
{
    public SqliteApplicationDbContext():base(){}
    public SqliteApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=app.db");
}