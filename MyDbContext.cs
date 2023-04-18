using Microsoft.EntityFrameworkCore;

namespace MyApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            dataentities = Set<DataEntity>();
        }

        public DbSet<DataEntity> dataentities { get; set; }
    }

    public class DataEntity
    {
        public int id { get; set; }
        public string? name { get; set; }
    }
}
