using Microsoft.EntityFrameworkCore;

namespace ASE3040.Infrastructure.Data;

public class SqliteApplicationDbContext : ApplicationDbContext
{
    public SqliteApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}