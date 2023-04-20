using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            dataentities = Set<DataEntity>();
            queries = Set<QueryEntity>();
        }

        public DbSet<DataEntity> dataentities { get; set; }
        public DbSet<QueryEntity> queries { get; set; }
    }

    public class DataEntity
    {
        public int id { get; set; }
        public string name { get; set; }

        public DataEntity() 
        {
            name = "";
        }
    }

    public class QueryEntity
    {
        public int id { get; set; }
        public string query { get; set; }
        public long executiontime { get; set; }

        public QueryEntity()
        {
            query = "";
        }
    }

    public class DataResponse 
    {
        public double avarageExecutionTime { get; set; }
        public List<QueryEntity> similarQueries { get; set; }
        public List<DataEntity> data { get; set; }

        public DataResponse()
        {
            similarQueries = new List<QueryEntity>();
            data = new List<DataEntity>();
        }
    }
}
